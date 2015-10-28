using System;
using System.Text;
using System.Windows.Forms;
using gr0ssSysTools.FileUtils;
using gr0ssSysTools.Utils;
using Microsoft.Win32;

namespace gr0ssSysTools
{
    public partial class AddRegistryKey : Form
    {
        private Settings _settings;

        public AddRegistryKey(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void AddRegistryKey_Load(object sender, EventArgs e)
        {
            RegistryKeyMethods.PopulateRootCombo(rootCombo);
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            var rootValue = RegistryKeyMethods.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3);

            MessageBox.Show($"The current registry key selected is:\n{rootValue}\\{fieldTextBox.Text}\n\nIt has a value of:\n{GetCurrentKeyValue()}", @"Current Value of Key",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var keyValue = GetCurrentKeyValue();

            if (keyValue == string.Empty)
                MessageBox.Show("You must first select a valid key.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            else
            {
                //var newRegistryKey = new Files.RegistryKey
                //{
                //    Root = RegistryKeyMethods.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3),
                //    Subkey = fieldTextBox.Text
                //};
                //_settings.RegistryKey = newRegistryKey;
                
                var newGeneralStruct = new GeneralStruct
                {
                    RegistryRoot = RegistryKeyMethods.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3),
                    RegistryField = fieldTextBox.Text
                };
                GeneralUtils.SaveGeneralSettings(newGeneralStruct);
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #region Populate Combos
        private void RootCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo2.Items.Clear();
            rootCombo2.Text = "";
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";
            RegistryKeyMethods.PopulateRootCombo2(rootCombo, rootCombo2);
        }
        
        private void RootCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";

            RegistryKeyMethods.PopulateRootCombo3(rootCombo, rootCombo2, rootCombo3);
        }
        #endregion Populate Combos

        private string GetCurrentKeyValue()
        {
            return (string) Registry.GetValue(RegistryKeyMethods.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3).ToString(), fieldTextBox.Text, "");
        }
    }
}
