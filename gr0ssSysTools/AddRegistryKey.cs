using System;
using System.Text;
using System.Windows.Forms;
using gr0ssSysTools.FileUtils;
using Microsoft.Win32;

namespace gr0ssSysTools
{
    public partial class AddRegistryKey : Form
    {
        private MiscUtils _utils;

        public AddRegistryKey()
        {
            InitializeComponent();
        }

        private void AddRegistryKey_Load(object sender, EventArgs e)
        {
            _utils = new MiscUtils();
            _utils.PopulateRootCombo(rootCombo);
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            var rootValue = _utils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3);

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
                var newGeneralStruct = new GeneralStruct
                {
                    RegistryRoot = _utils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3),
                    RegistryField = fieldTextBox.Text
                };
                var dir = new DirectoryUtils();
                dir.SaveGeneralStructToFile(newGeneralStruct);
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
            _utils.PopulateRootCombo2(rootCombo, rootCombo2);
        }
        
        private void RootCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";

            _utils.PopulateRootCombo3(rootCombo, rootCombo2, rootCombo3);
        }
        #endregion Populate Combos

        private string GetCurrentKeyValue()
        {
            return (string) Registry.GetValue(_utils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3).ToString(), fieldTextBox.Text, "");
        }
    }
}
