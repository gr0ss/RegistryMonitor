using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RegistryMonitor.Files;
using RegistryMonitor.FileUtils;

namespace RegistryMonitor
{
    public partial class AddName : Form
    {

        private const string ENV_TEXT = "Name of new environment:";
        private const string TOOL_TEXT = "Name of new tool:";
        private bool _isEnv;
        private LoadedSettings _loadedSettings;

        public AddName(bool envTab, LoadedSettings loadedSettings)
        {
            InitializeComponent();
            _isEnv = envTab;
            _loadedSettings = loadedSettings;
            textLabel.Text = envTab ? ENV_TEXT : TOOL_TEXT;
        }

        private void AddName_Load(object sender, EventArgs e)
        {
            nameTextbox.Focus();
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_isEnv)
            {
                _loadedSettings.Environments.Add(CreateNewEnvironment());
            }
            else
            {
                _loadedSettings.Tools.Add(CreateNewTool());
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private LoadedEnvironments CreateNewEnvironment()
        {
            var newName = nameTextbox.Text;
            var currentHotkeys = _loadedSettings.Environments.Select(env => env.HotKey).ToList();
            var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(newName,currentHotkeys).ToString();
            var iconLabel = newName[0].ToString().ToLower() +
                            newName[1].ToString().ToLower() +
                            newName[2].ToString().ToLower();
            return new LoadedEnvironments
            {
                ID = Guid.NewGuid(),
                Name = newName,
                SubkeyValue = "Data\\DB.xml",
                HotKey = newUniqueHotkey,
                IconLabel = iconLabel,
                IconTextColor = "White",
                IconBackgroundColor = "Blue"
            };
        }

        private LoadedTools CreateNewTool()
        {
            var newName = nameTextbox.Text;
            var currentHotkeys = _loadedSettings.Tools.Select(env => env.HotKey).ToList();
            var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(newName, currentHotkeys).ToString();
            return new LoadedTools
            {
                ID = Guid.NewGuid(),
                Name = newName,
                FileLocation = Directory.GetCurrentDirectory(),
                HotKey = newUniqueHotkey
            };
        }
    }
}
