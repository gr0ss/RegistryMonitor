using System;
using System.IO;
using System.Linq;
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
        private Settings _settings;

        public AddName(bool envTab, Settings settings)
        {
            InitializeComponent();
            _isEnv = envTab;
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
                _settings.Environments.Add(CreateNewEnvironment());
            }
            else
            {
                _settings.Tools.Add(CreateNewTool());
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
            var currentHotkeys = _settings.Environments.Select(env => env.HotKey).ToList();
            var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(newName,currentHotkeys).ToString();
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
            var currentHotkeys = _settings.Tools.Select(env => env.HotKey).ToList();
            var newUniqueHotkey = MiscUtils.GetFirstUniqueHotkey(newName, currentHotkeys).ToString();
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
