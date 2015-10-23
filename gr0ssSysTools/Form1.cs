using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using RegistryUtils;
using GlobalHotKey;
using Zhwang.SuperNotifyIcon;
using gr0ssSysTools.FileUtils;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading.Tasks;
using FlimFlan.IconEncoder;
using System.Drawing.Imaging;
using System.IO;

namespace gr0ssSysTools
{
    public partial class Form1 : Form
    {
        private const string USER_ROOT = "HKEY_CURRENT_USER\\Software\\PSI";
        readonly Font _font = new Font("Arial Narrow", 7.0f);

        private DirectoryUtils _dir;
        private string _environmentsText = "\\environments.txt";
        private string _toolsText = "\\tools.txt";
        private string _generalText = "\\general.txt";
        private List<FileStruct> _environments;
        private List<FileStruct> _tools;
        private GeneralStruct _general;

        private MiscUtils _util;

        private static Configuration _config;

        private FileStruct _currentEnvironment;

        private RegistryMonitor _registryMonitor;
        private readonly HotKeyManager _hkManager;
        private readonly ContextMenuStrip _menuStrip;
        private readonly Point? _locationOfIcon;

        public Form1()
        {
            InitializeComponent();
            
            _dir = new DirectoryUtils();
            _util = new MiscUtils();
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            LoadTextFiles();
            LoadMenu();
            CheckRegistryKeyExists();

            _hkManager = new HotKeyManager();
            _hkManager.KeyPressed += HkManagerOnKeyPressed;
            _hkManager.Register(Key.Z, System.Windows.Input.ModifierKeys.Windows | System.Windows.Input.ModifierKeys.Alt);
            _menuStrip = menuStrip;

            SuperNotifyIcon superNotifyIcon = new SuperNotifyIcon {NotifyIcon = Icon};
            _locationOfIcon = superNotifyIcon.GetLocation();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            _currentEnvironment = _environments.Find(env => env.ValueKey == (string)Registry.GetValue(USER_ROOT, "DBPath", ""));
            SetIcon();

            var keyName = $"{Registry.CurrentUser.Name}\\Software\\PSI";
            
            _registryMonitor = new RegistryMonitor(keyName)
            {
                RegChangeNotifyFilter = RegChangeNotifyFilter.Value
            };
            _registryMonitor.RegChanged += OnRegChanged;
            _registryMonitor.Error += OnError;
            _registryMonitor.Start();
        }

        private void CheckRegistryKeyExists()
        {
            _general = _dir.ReadFileandPopulateGeneralStruct();
            if (_general.RegistryRoot == string.Empty)
            {
                var addRegistry = new AddRegistryKey();
                addRegistry.Show();
            }
        }

        private void AddRegistryKeyToAppConfig()
        {
            
            _config.AppSettings.Settings.Add("ModificationDate",
                           DateTime.Now.ToLongTimeString() + " ");

            // Save the changes in App.config file.

            _config.Save(ConfigurationSaveMode.Modified);
        }
        
        private void LoadTextFiles()
        {
            _dir.CreateTextIfItDoesntExist(_environmentsText);
            _dir.CreateTextIfItDoesntExist(_toolsText);
            _dir.CreateTextIfItDoesntExist(_generalText);
        }

        private void LoadMenu()
        {
            _environments = _dir.ReadFileAndPopulateList(_environmentsText);
            _tools = _dir.ReadFileAndPopulateList(_toolsText);

            menuStrip.Items.Clear();
            menuTools.DropDownItems.Clear();

            foreach (var tool in _tools)
            {
                menuTools.DropDownItems.Add(_util.GetNameWithHotkey(tool.Name, tool.HotKey));
                menuTools.DropDownItems[menuTools.DropDownItems.Count - 1].Click += ToolClicked;
            }
            
            menuStrip.Items.Add(menuTools);
            menuStrip.Items.Add(toolStripSeparator1);
            
            foreach (var env in _environments)
            {
                menuStrip.Items.Add(_util.GetNameWithHotkey(env.Name, env.HotKey));

                menuStrip.Items[menuStrip.Items.Count - 1].Click += EnvironmentClicked;
            }

            menuStrip.Items.Add(new ToolStripSeparator());
            menuStrip.Items.Add(menuEdit);
            menuStrip.Items.Add(menuExit);
        }

        #region OnClick
        private void EnvironmentClicked(object sender, EventArgs e)
        {
            _currentEnvironment = _environments.Find(env => env.Name == sender.ToString().Replace("&", ""));
            SetRegistry();
        }

        private void ToolClicked(object sender, EventArgs e)
        {
            var currentTool = _tools.Find(tool => tool.Name == sender.ToString().Replace("&", ""));
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter the executable to run, including the complete path
            start.FileName = currentTool.ValueKey;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;
            
            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                 proc.WaitForExit();

                 // Retrieve the app's exit code
                 exitCode = proc.ExitCode;
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
            Edit edit = new Edit(true);
            edit.Closed += (o, args) => LoadMenu();
            edit.Show();
        }
        #endregion OnClick

        #region Registry
        private void SetRegistry()
        {
            Registry.SetValue(USER_ROOT, "DBPath", _currentEnvironment.ValueKey);
            SetIcon();
        }
        
        private void OnRegChanged(object sender, EventArgs e)
        {
            _currentEnvironment = _environments.Find(env => env.ValueKey == (string)Registry.GetValue(USER_ROOT, "DBPath", ""));
            SetIcon();
            ShowEnvironmentChangedBalloonTip();
        }

        private void SetIcon()
        {
            Task.Run(() =>
            {
                Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			    using (Graphics g = Graphics.FromImage(bmp))
			    {
                    Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			        g.FillEllipse(_currentEnvironment.IconColor, rectangle);
                    g.DrawString(_currentEnvironment.IconLabel, _font, Brushes.White, 0, 0);
			    }

                Icon.Icon = Converter.BitmapToIcon(bmp);
            });
        }

        private void ShowEnvironmentChangedBalloonTip()
        {
            Icon.BalloonTipTitle = "Environment Has Changed";
            Icon.BalloonTipText = $"The environment has been changed to {_currentEnvironment.Name}";
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
