using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FlimFlan.IconEncoder;
using gr0ssSysTools.Files;
using gr0ssSysTools.FileUtils;
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
                var newUserMessage = MessageBox.Show("As this is your first time running the program, we need you to select the registry key you would like to monitor.",
                    "New User Registry Monitoring Setup",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (newUserMessage == DialogResult.OK)
                {
                    var addRegistry = new AddRegistryKey(_loadedSettings);
                    addRegistry.FormClosing += RegistryKeyAdded_EventHandler;
                    addRegistry.Show();
                }
                else
                    Environment.Exit(0);                
            }
            else
                LoadRegistryMonitor();
        }

        private void RegistryKeyAdded_EventHandler(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(_loadedSettings.MonitoredRegistryKey.Root))
                Environment.Exit(0);
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
                MessageBox.Show($"Couldn't find anything to run at {currentTool.FileLocation}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            Environment.Exit(0);
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
            Settings settings = new Settings(_loadedSettings, true);
            settings.Closed += (o, args) =>
            {
                SetNewRegistrykeyIfChanged();
                SetNewGlobalHotkeyIfChanged();
                LoadMenu();
                SetIcon();
            };
            settings.Show();
        }
        #endregion OnClick

        #region Registry
        private void SetRegistry()
        {
            if (!string.IsNullOrEmpty(_loadedSettings.MonitoredRegistryKey.Root))
            {
                Registry.SetValue(_loadedSettings.MonitoredRegistryKey.Root, _loadedSettings.MonitoredRegistryKey.Subkey, _currentLoadedEnvironment.SubkeyValue);
                SetIcon();
            }
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
            var iconFont = _loadedSettings.General.IconFont;
            var fontSize = _loadedSettings.General.IconFontSize;
            var iconColor = new SolidBrush(Color.FromName(_currentLoadedEnvironment.IconColor));

            Font font = new Font(iconFont, fontSize);
            Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			using (Graphics g = Graphics.FromImage(bmp))
			{
                Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			    g.FillEllipse(iconColor, rectangle);
                g.DrawString(_currentLoadedEnvironment.IconLabel, font, Brushes.White, 0, 0);
			}

            Icon.Icon = Converter.BitmapToIcon(bmp);
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

            Icon.BalloonTipTitle = "Environment Has Changed";
            Icon.BalloonTipText = $"The environment has been changed to {_currentLoadedEnvironment.Name}";
            Icon.ShowBalloonTip(3000);
        }

        private void OnError(object sender, ErrorEventArgs e)
		{
            if (InvokeRequired)
            {
                BeginInvoke(new ErrorEventHandler(OnError), new object[] { sender, e });
                return;
            }

            MessageBox.Show("Error: " + e.GetException().InnerException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine("Error: " + e.GetException().InnerException);
		}
        #endregion Registry
    }
}
