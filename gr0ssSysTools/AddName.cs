using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gr0ssSysTools.FileUtils;

namespace gr0ssSysTools
{
    public partial class AddName : Form
    {

        private const string ENV_TEXT = "Name of new environment:";
        private const string TOOL_TEXT = "Name of new tool:";
        private bool _isEnv;
        private List<FileStruct> _list; 

        public AddName(bool envTab, List<FileStruct> list)
        {
            InitializeComponent();
            _isEnv = envTab;
            _list = list;
            if (envTab)
                textLabel.Text = ENV_TEXT;
            else
                textLabel.Text = TOOL_TEXT;
        }

        private void AddName_Load(object sender, EventArgs e)
        {
            nameTextbox.Focus();
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_isEnv)
                EnvironmentUtils.AddNewEnvironmentSetting(nameTextbox.Text, _list);
            else
                ToolsUtils.AddNewToolSetting(nameTextbox.Text, _list);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
