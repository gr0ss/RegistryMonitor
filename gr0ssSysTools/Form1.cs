﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FlimFlan.IconEncoder;
using gr0ssSysTools.ExtensionMethods;
using gr0ssSysTools.Files;
using gr0ssSysTools.FileUtils;
using gr0ssSysTools.Properties;
using gr0ssSysTools.Utils;
using GlobalHotKey;
using Microsoft.Win32;
using RegistryUtils;
using Zhwang.SuperNotifyIcon;

namespace gr0ssSysTools
{
    public partial class Form1 : Form
    {
        private LoadedSettings _loadedSettings;
        
        private LoadedEnvironments _currentLoadedEnvironment;
        private LoadedGlobalHotkey _currentLoadedGlobalHotkey;
        private MonitoredRegistryKey _currentMonitoredRegistryKey;
        
        private RegistryMonitor _registryMonitor;
        private readonly HotKeyManager _hkManager;
        private readonly ContextMenuStrip _menuStrip;
        private readonly SuperNotifyIcon _superNotifyIcon;
        private readonly Point? _locationOfIcon;

        private bool _settingsAlreadyRunning;

        public Form1()
        {
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
                var newUserMessage = MessageBox.Show(Resources.Select_Registry_Key_To_Monitor,
                    Resources.Select_Registry_Key_To_Monitor_Caption,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (newUserMessage == DialogResult.OK)
                {
                    var addRegistry = new AddRegistryKey(_loadedSettings);
                    addRegistry.FormClosing += RegistryKeyAdded_EventHandler;
                    addRegistry.Show();
                }
                else
                    Environment.Exit(Constants.EnvironmentExitCodes.NoRegistryKey);                
            }
            else
                LoadRegistryMonitor();
        }

        private void RegistryKeyAdded_EventHandler(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(_loadedSettings.MonitoredRegistryKey.Root))
                Environment.Exit(Constants.EnvironmentExitCodes.NoRegistryKey);
            else
                LoadRegistryMonitor();
        }

        private void LoadRegistryMonitor()
        {
            _currentLoadedEnvironment = _loadedSettings.Environments.Find(env => env.SubkeyValue == (string)Registry.GetValue(_loadedSettings.MonitoredRegistryKey.Root, _loadedSettings.MonitoredRegistryKey.Subkey, ""));
            SetIcon();
            
            _registryMonitor = new RegistryMonitor(_loadedSettings.MonitoredRegistryKey.Root)
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
            
            foreach (var env in _loadedSettings.Environments)
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
            _currentLoadedEnvironment = _loadedSettings.Environments.Find(env => env.Name == sender.ToString().Replace("&", ""));
            SetRegistry();
        }

        private void ToolClicked(object sender, EventArgs e)
        {
            var currentTool = _loadedSettings.Tools.Find(tool => tool.Name == sender.ToString().Replace("&", ""));
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter the executable to run, including the complete path
            start.FileName = currentTool.FileLocation;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;
            
            if (File.Exists(currentTool.FileLocation))
            {
                // Run the external process & wait for it to finish
                using (Process proc = Process.Start(start))
                {
                     proc.WaitForExit();

                     // Retrieve the app's exit code
                     exitCode = proc.ExitCode;
                }
            }
            else
            {
                MessageBox.Show($"Couldn't find anything to run at {currentTool.FileLocation}", Resources.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (_currentLoadedEnvironment.LoadIcon && 
                File.Exists(_currentLoadedEnvironment.IconFileLocation) &&
                _currentLoadedEnvironment.IconFileLocation.Contains(Constants.FileExtensions.IconExtension, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var iconFromFile = new Icon(_currentLoadedEnvironment.IconFileLocation, 16, 16);
                    Icon.Icon = iconFromFile;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.Error_Loading_Icon + ex, Resources.Error_Loading_Icon_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Icon.Icon = Resources.Exit_16;
                }
            }
            else
            {
                Font font = new Font(_loadedSettings.General.IconFont, _loadedSettings.General.IconFontSize);
                Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			    using (Graphics g = Graphics.FromImage(bmp))
			    {
                    Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			        g.FillEllipse(_currentLoadedEnvironment.IconBackgroundColor.ToSolidBrush(), rectangle);
                    g.DrawString(_currentLoadedEnvironment.IconLabel, font, _currentLoadedEnvironment.IconTextColor.ToSolidBrush(), 0, 1);
			    }

                Icon.Icon = Converter.BitmapToIcon(bmp);
            }
        }

        private void SetNewGlobalHotkeyIfChanged()
        {
            if (_currentLoadedGlobalHotkey == _loadedSettings.General.LoadedGlobalHotkey) return;

            GlobalHotkeyUtils.UnregisterGlobalHotkey(_hkManager, _currentLoadedGlobalHotkey);
            GlobalHotkeyUtils.RegisterGlobalHotkey(_hkManager, _loadedSettings.General.LoadedGlobalHotkey);
            _currentLoadedGlobalHotkey = _loadedSettings.General.LoadedGlobalHotkey;
        }

        private void SetNewRegistrykeyIfChanged()
        {
            if (_currentMonitoredRegistryKey == _loadedSettings.MonitoredRegistryKey) return;

            _registryMonitor.Stop();
            _registryMonitor.Dispose();

            _registryMonitor = new RegistryMonitor(_loadedSettings.MonitoredRegistryKey.Root)
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
            Icon.ShowBalloonTip(1000);
        }

        private void OnError(object sender, ErrorEventArgs e)
		{
            if (InvokeRequired)
            {
                BeginInvoke(new ErrorEventHandler(OnError), sender, e);
                return;
            }

            MessageBox.Show(Resources.Error + e.GetException().InnerException, Resources.Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine(Resources.Error + e.GetException().InnerException);
		}
        #endregion Registry
    }
}
