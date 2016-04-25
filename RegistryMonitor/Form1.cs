using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Exceptionless;
using Exceptionless.Logging;
using GlobalHotKey;
using Microsoft.Win32;
using RegistryMonitor.Files;
using RegistryMonitor.FileUtils;
using RegistryMonitor.Utils;
using RegistryUtils;
using Zhwang.SuperNotifyIcon;

namespace RegistryMonitor
{
    /// <summary>
    /// Main Class for RegistryMonitor.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly LoadedSettings _loadedSettings;
        
        private LoadedEnvironments _currentLoadedEnvironment;
        private LoadedGlobalHotkey _currentLoadedGlobalHotkey;
        private MonitoredRegistryKey _currentMonitoredRegistryKey;
        
        private RegistryUtils.RegistryMonitor _registryMonitor;
        private readonly HotKeyManager _hkManager;
        private readonly ContextMenuStrip _menuStrip;
        private readonly SuperNotifyIcon _superNotifyIcon;
        private readonly Point? _locationOfIcon;

        private bool _settingsAlreadyRunning;
        private bool _allowLogging = true;
        private bool _allowFeatureUseLogging = true;

        /// <summary>
        /// Main Class for RegistryMonitor.
        /// </summary>
        public Form1()
        {
            if (_allowLogging)
            {
                ExceptionlessClient.Default.Register(false);
                ExceptionlessClient.Default.Configuration.SetUserIdentity(Environment.MachineName);
                ExceptionlessClient.Default.Configuration.UseSessions();
            }
            
            InitializeComponent();
            
            _loadedSettings = new LoadedSettings();

            LoadMenu();
            _hkManager = new HotKeyManager();
            LoadGlobalHotkey();
            _menuStrip = menuStrip;

            // Gets the location of the systray icon
            _superNotifyIcon = new SuperNotifyIcon {NotifyIcon = Icon};
            _locationOfIcon = _superNotifyIcon.GetLocation();

            _settingsAlreadyRunning = false;
        }

        private void LoadGlobalHotkey()
        {
            _hkManager.KeyPressed += HkManagerOnKeyPressed;
            GlobalHotkeyUtils.RegisterGlobalHotkey(_hkManager, _loadedSettings.General.LoadedGlobalHotkey);
            _currentLoadedGlobalHotkey = _loadedSettings.General.LoadedGlobalHotkey;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckRegistryKeyExists();
        }

        private void CheckRegistryKeyExists()
        {
            if (string.IsNullOrEmpty(_loadedSettings.MonitoredRegistryKey.Root))
            {
                var newUserMessage = MessageBox.Show(Constants.RegistryKeyMessages.SelectRegistryKeyToMonitor,
                                                     Constants.RegistryKeyMessages.SelectRegistryKeyToMonitorCaption,
                                                     MessageBoxButtons.OKCancel,
                                                     MessageBoxIcon.Information);

                if (newUserMessage == DialogResult.OK)
                {
                    var addRegistry = new AddRegistryKey(_loadedSettings);
                    addRegistry.FormClosing += RegistryKeyAdded_EventHandler;
                    addRegistry.Show();
                }
                else
                {
                    if (_allowLogging) ExceptionlessClient.Default.SubmitLog(Constants.LogMessages.NoRegistryKey, LogLevel.Info);
                    Environment.Exit(Constants.EnvironmentExitCodes.NoRegistryKey);
                }                
            }
            else
                LoadRegistryMonitor();
        }

        private void RegistryKeyAdded_EventHandler(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(_loadedSettings.MonitoredRegistryKey.Root))
            {
                if (_allowLogging) ExceptionlessClient.Default.SubmitLog(Constants.LogMessages.EmptyRegistryKey, LogLevel.Info);
                Environment.Exit(Constants.EnvironmentExitCodes.NoRegistryKey);
            }
            else
            {
                LoadRegistryMonitor();
            }
        }

        private void LoadRegistryMonitor()
        {
            _currentLoadedEnvironment = _loadedSettings.Environments.Find(env => env.SubkeyValue == (string)Registry.GetValue(_loadedSettings.MonitoredRegistryKey.Root, _loadedSettings.MonitoredRegistryKey.Subkey, ""));
            SetIcon();
            
            _registryMonitor = new RegistryUtils.RegistryMonitor(_loadedSettings.MonitoredRegistryKey.Root)
            {
                RegChangeNotifyFilter = RegChangeNotifyFilter.Value
            };
            _registryMonitor.RegChanged += OnRegChanged;
            _registryMonitor.Error += OnError;
            _registryMonitor.Start();
            _currentMonitoredRegistryKey = _loadedSettings.MonitoredRegistryKey;
        }
        
        private void LoadMenu()
        {
            menuStrip.Items.Clear();
            menuTools.DropDownItems.Clear();

            foreach (var tool in _loadedSettings.Tools)
            {
                menuTools.DropDownItems.Add(MiscUtils.GetNameWithHotkey(tool.Name, tool.HotKey));
                menuTools.DropDownItems[menuTools.DropDownItems.Count - 1].Click += ToolClicked;
            }
            
            menuStrip.Items.Add(menuTools);
            menuStrip.Items.Add(toolStripSeparator1);
            
            foreach (var env in _loadedSettings.Environments.Where(env => env.DisplayOnMenu))
            {
                menuStrip.Items.Add(MiscUtils.GetNameWithHotkey(env.Name, env.HotKey));

                menuStrip.Items[menuStrip.Items.Count - 1].Click += EnvironmentClicked;
            }

            menuStrip.Items.Add(new ToolStripSeparator());
            menuStrip.Items.Add(menuEdit);
            menuStrip.Items.Add(menuExit);
        }

        #region OnClick
        private void EnvironmentClicked(object sender, EventArgs e)
        {
            if (_allowFeatureUseLogging) ExceptionlessClient.Default.SubmitFeatureUsage(Constants.FeatureUsages.EnvironmentChanged);

            _currentLoadedEnvironment = _loadedSettings.Environments.Find(env => env.Name == sender.ToString().Replace("&", ""));
            SetRegistry();
        }

        private void ToolClicked(object sender, EventArgs e)
        {
            if (_allowFeatureUseLogging) ExceptionlessClient.Default.SubmitFeatureUsage(Constants.FeatureUsages.ToolUsed);

            var currentTool = _loadedSettings.Tools.Find(tool => tool.Name == sender.ToString().Replace("&", ""));

            if (File.Exists(currentTool.FileLocation))
            {
                var toolToRun = new Process
                {
                    StartInfo = {FileName = currentTool.FileLocation}
                };
                toolToRun.Start();
            }
            else
            {
                if (_allowLogging) ExceptionlessClient.Default.SubmitLog(Constants.LogMessages.ToolDidntExist, LogLevel.Info);

                MessageBox.Show($"{Constants.ToolMessages.CouldntFindTool}{currentTool.FileLocation}", 
                                   Constants.ToolMessages.CouldntFindToolCaption, 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HkManagerOnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            _menuStrip.Show();
            if (_locationOfIcon != null)
                _menuStrip.Location = (Point)_locationOfIcon;
            _menuStrip.Focus();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAndDisposeProcessesAndEvents();
            
            Environment.Exit(Constants.EnvironmentExitCodes.Success);
        }

        private void StopAndDisposeProcessesAndEvents()
        {
            _registryMonitor.Stop();
            _registryMonitor.Dispose();
            _hkManager.Dispose();
            _menuStrip.Dispose();
            _superNotifyIcon.Dispose();
            Icon.Dispose();
        }

        private void menuEdit_Click(object sender, EventArgs e)
        {
            if (_settingsAlreadyRunning) return;
            
            if (_allowFeatureUseLogging) ExceptionlessClient.Default.SubmitFeatureUsage(Constants.FeatureUsages.UpdateSettings);

            Settings settings = new Settings(_loadedSettings);
            settings.Closed += (o, args) =>
            {
                SetNewRegistrykeyIfChanged();
                SetNewGlobalHotkeyIfChanged();
                LoadMenu();
                SetIcon();
                _settingsAlreadyRunning = false;
            };
            _settingsAlreadyRunning = true;
            settings.Show();
        }
        #endregion OnClick

        #region Registry
        private void SetRegistry()
        {
            if (string.IsNullOrEmpty(_loadedSettings.MonitoredRegistryKey.Root)) return;
            Registry.SetValue(_loadedSettings.MonitoredRegistryKey.Root, _loadedSettings.MonitoredRegistryKey.Subkey, _currentLoadedEnvironment.SubkeyValue);
            SetIcon();
        }
        
        private void OnRegChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_loadedSettings.MonitoredRegistryKey.Root)) return;

            _currentLoadedEnvironment = _loadedSettings.Environments.Find(env => env.SubkeyValue == (string)Registry.GetValue(_loadedSettings.MonitoredRegistryKey.Root, _loadedSettings.MonitoredRegistryKey.Subkey, ""));
            SetIcon();
            ShowEnvironmentChangedBalloonTip();
        }

        private void SetIcon()
        {
            IconUtils.SetIcon(_currentLoadedEnvironment, _loadedSettings, Icon);
        }

        private void SetNewGlobalHotkeyIfChanged()
        {
            GlobalHotkeyUtils.SetNewGlobalHotkeyIfChanged(_loadedSettings, _currentLoadedGlobalHotkey, _hkManager);
        }

        private void SetNewRegistrykeyIfChanged()
        {
            if (_currentMonitoredRegistryKey == _loadedSettings.MonitoredRegistryKey) return;

            _registryMonitor.Stop();
            _registryMonitor.Dispose();

            _registryMonitor = new RegistryUtils.RegistryMonitor(_loadedSettings.MonitoredRegistryKey.Root)
            {
                RegChangeNotifyFilter = RegChangeNotifyFilter.Value
            };
            _registryMonitor.RegChanged += OnRegChanged;
            _registryMonitor.Error += OnError;
            _registryMonitor.Start();
            _currentMonitoredRegistryKey = _loadedSettings.MonitoredRegistryKey;
        }

        private void ShowEnvironmentChangedBalloonTip()
        {
            if (!_loadedSettings.General.ShowBalloonTips) return;

            Icon.BalloonTipTitle = Constants.BalloonTips.IconTitle;
            Icon.BalloonTipText = $"{Constants.BalloonTips.IconCaption} {_currentLoadedEnvironment.Name}";
            Icon.ShowBalloonTip(500);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            e.GetException().ToExceptionless().Submit();

            if (InvokeRequired)
            {
                BeginInvoke(new ErrorEventHandler(OnError), sender, e);
                return;
            }

            MessageBox.Show(Constants.Messages.Error + e.GetException().InnerException, 
                            Constants.Messages.ErrorCaption, 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine(Constants.Messages.Error + e.GetException().InnerException);
		}
        #endregion Registry
    }
}
