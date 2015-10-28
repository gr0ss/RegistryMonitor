using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using FlimFlan.IconEncoder;
using gr0ssSysTools.FileUtils;
using GlobalHotKey;
using Microsoft.Win32;
using RegistryUtils;
using Zhwang.SuperNotifyIcon;

namespace gr0ssSysTools
{
    public partial class Form1 : Form
    {
        private Settings _settings;
        
        private static Configuration _config;
        
        private Files.Environments _currentEnvironment;
        
        private RegistryMonitor _registryMonitor;
        private readonly HotKeyManager _hkManager;
        private readonly ContextMenuStrip _menuStrip;
        private readonly Point? _locationOfIcon;

        public Form1()
        {
            InitializeComponent();
            
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _settings = new Settings();

            LoadMenu();
            _hkManager = new HotKeyManager();
            _hkManager.KeyPressed += HkManagerOnKeyPressed;
            _hkManager.Register(Key.Z, System.Windows.Input.ModifierKeys.Windows | System.Windows.Input.ModifierKeys.Alt);
            _menuStrip = menuStrip;

            SuperNotifyIcon superNotifyIcon = new SuperNotifyIcon {NotifyIcon = Icon};
            _locationOfIcon = superNotifyIcon.GetLocation();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckRegistryKeyExists();
        }

        private void CheckRegistryKeyExists()
        {
            if (string.IsNullOrEmpty(_settings.RegistryKey.Root))
            {
                var newUserMessage = MessageBox.Show("Thank you for downloading my program!\n\nAs this is your first time running the program, we need you to select the registry key you would like to monitor.",
                    "New User Registry Monitoring Setup",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (newUserMessage == DialogResult.OK)
                {
                    var addRegistry = new AddRegistryKey(_settings);
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
            if (string.IsNullOrEmpty(_settings.RegistryKey.Root))
                Environment.Exit(0);
            else
                LoadRegistryMonitor();
        }

        private void LoadRegistryMonitor()
        {
            _currentEnvironment = _settings.Environments.Find(env => env.SubkeyValue == (string)Registry.GetValue(_settings.RegistryKey.Root, _settings.RegistryKey.Subkey, ""));
            SetIcon();
            
            _registryMonitor = new RegistryMonitor(_settings.RegistryKey.Root)
            {
                RegChangeNotifyFilter = RegChangeNotifyFilter.Value
            };
            _registryMonitor.RegChanged += OnRegChanged;
            _registryMonitor.Error += OnError;
            _registryMonitor.Start();
        }
        
        private void LoadMenu()
        {
            menuStrip.Items.Clear();
            menuTools.DropDownItems.Clear();

            foreach (var tool in _settings.Tools)
            {
                menuTools.DropDownItems.Add(MiscUtils.GetNameWithHotkey(tool.Name, tool.HotKey));
                menuTools.DropDownItems[menuTools.DropDownItems.Count - 1].Click += ToolClicked;
            }
            
            menuStrip.Items.Add(menuTools);
            menuStrip.Items.Add(toolStripSeparator1);
            
            foreach (var env in _settings.Environments)
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
            _currentEnvironment = _settings.Environments.Find(env => env.Name == sender.ToString().Replace("&", ""));
            SetRegistry();
        }

        private void ToolClicked(object sender, EventArgs e)
        {
            var currentTool = _settings.Tools.Find(tool => tool.Name == sender.ToString().Replace("&", ""));
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
                MessageBox.Show($"Couldn't find anything at {currentTool.FileLocation} to run", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _registryMonitor.Stop();
            _hkManager.Dispose();

            Close();
        }

        private void menuEdit_Click(object sender, EventArgs e)
        {
            Edit edit = new Edit(_settings, true);
            edit.Closed += (o, args) => LoadMenu();
            edit.Show();
        }
        #endregion OnClick

        #region Registry
        private void SetRegistry()
        {
            if (!string.IsNullOrEmpty(_settings.RegistryKey.Root))
            {
                Registry.SetValue(_settings.RegistryKey.Root, _settings.RegistryKey.Subkey, _currentEnvironment.SubkeyValue);
                SetIcon();
            }
        }
        
        private void OnRegChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_settings.RegistryKey.Root))
            {
                _currentEnvironment = _settings.Environments.Find(env => env.SubkeyValue == (string)Registry.GetValue(_settings.RegistryKey.Root, _settings.RegistryKey.Subkey, ""));
                SetIcon();
                ShowEnvironmentChangedBalloonTip();
            }
        }

        private void SetIcon()
        {
            var iconFont = _settings.General.IconFont;
            var fontSize = _settings.General.IconFontSize;
            var iconColor = new SolidBrush(Color.FromName(_currentEnvironment.IconColor));

            Font font = new Font(iconFont, fontSize);
            Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			using (Graphics g = Graphics.FromImage(bmp))
			{
                Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			    g.FillEllipse(iconColor, rectangle);
                g.DrawString(_currentEnvironment.IconLabel, font, Brushes.White, 0, 0);
			}

            Icon.Icon = Converter.BitmapToIcon(bmp);
        }

        private void ShowEnvironmentChangedBalloonTip()
        {
            if (_settings.General.ShowBalloonTips)
            {
                Icon.BalloonTipTitle = "Environment Has Changed";
                Icon.BalloonTipText = $"The environment has been changed to {_currentEnvironment.Name}";
                Icon.ShowBalloonTip(3000);
            }
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
