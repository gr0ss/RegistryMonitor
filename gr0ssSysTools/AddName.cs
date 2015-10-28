using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gr0ssSysTools.Files;
using gr0ssSysTools.FileUtils;

namespace gr0ssSysTools
{
    public partial class AddName : Form
    {

        private const string ENV_TEXT = "Name of new environment:";
        private const string TOOL_TEXT = "Name of new tool:";
        private bool _isEnv;
        private List<FileStruct> _list; // Remove after Settings are implemented.
        private Settings _settings;

        public AddName(bool envTab, List<FileStruct> list, Settings settings)
        {
            InitializeComponent();
            _isEnv = envTab;
            _list = list; // Remove after Settings are implemented.
            _settings = settings;
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
                //_settings.Environments.Add(CreateNewEnvironment());
                // Remove next line after Settings are implemented.
                EnvironmentUtils.AddNewEnvironmentSetting(nameTextbox.Text, _list);
            }
            else
            {
                //_settings.Tools.Add(CreateNewTool());
                // Remove next line after Settings are implemented.
                ToolsUtils.AddNewToolSetting(nameTextbox.Text, _list);
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Environments CreateNewEnvironment()
        {
            var newName = nameTextbox.Text;
            var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(newName, MiscUtils.GetAllEnvironmentsHotkeys(_settings.Environments)).ToString();
            var iconLabel = newName[0].ToString().ToLower() +
                            newName[1].ToString().ToLower() +
                            newName[2].ToString().ToLower();
            return new Environments
            {
                ID = Guid.NewGuid(),
                Name = newName,
                SubkeyValue = "Data\\DB.xml",
                HotKey = newUniqueHotkey,
                IconLabel = iconLabel,
                IconColor = "Blue"
            };
        }

        private Tools CreateNewTool()
        {
            var newName = nameTextbox.Text;
            var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(newName, MiscUtils.GetAllToolsHotkeys(_settings.Tools)).ToString();
            return new Tools
            {
                ID = Guid.NewGuid(),
                Name = newName,
                FileLocation = Directory.GetCurrentDirectory(),
                HotKey = newUniqueHotkey
            };
        }
    }
}
