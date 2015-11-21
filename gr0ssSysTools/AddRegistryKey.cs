using System;
using System.Windows.Forms;
using gr0ssSysTools.FileUtils;
using gr0ssSysTools.Properties;
using gr0ssSysTools.Utils;
using Microsoft.Win32;

namespace gr0ssSysTools
{
    public partial class AddRegistryKey : Form
    {
        private LoadedSettings _loadedSettings;

        public AddRegistryKey(LoadedSettings loadedSettings)
        {
            InitializeComponent();
            _loadedSettings = loadedSettings;
        }

        private void AddRegistryKey_Load(object sender, EventArgs e)
        {
            RegistryKeyUtils.PopulateRootCombo(rootCombo);
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            var rootValue = RegistryKeyUtils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3);

            MessageBox.Show($"The current registry key selected is:\n{rootValue}\\{fieldTextBox.Text}\n\nIt has a value of:\n{GetCurrentKeyValue()}", 
                @"Current Value of Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var keyValue = GetCurrentKeyValue();

            if (keyValue == string.Empty)
                MessageBox.Show(Resources.Select_Registry_Key, Resources.Select_Registry_Key_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                var newRegistryKey = new Files.MonitoredRegistryKey
                {
                    Root = RegistryKeyUtils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3),
                    Subkey = fieldTextBox.Text
                };
                _loadedSettings.MonitoredRegistryKey = newRegistryKey;
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(Constants.EnvironmentExitCodes.FailedToFindRegistryKey);
        }

        #region Populate Combos
        private void RootCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo2.Items.Clear();
            rootCombo2.Text = "";
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";
            RegistryKeyUtils.PopulateRootCombo2(rootCombo, rootCombo2);
        }
        
        private void RootCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";

            RegistryKeyUtils.PopulateRootCombo3(rootCombo, rootCombo2, rootCombo3);
        }
        #endregion Populate Combos

        private string GetCurrentKeyValue()
        {
            return (string) Registry.GetValue(RegistryKeyUtils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3).ToString(), fieldTextBox.Text, "");
        }
    }
}
