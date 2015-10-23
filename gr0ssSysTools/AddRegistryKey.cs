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
using Microsoft.Win32;

namespace gr0ssSysTools
{
    public partial class AddRegistryKey : Form
    {
        public AddRegistryKey()
        {
            InitializeComponent();
        }

        private void AddRegistryKey_Load(object sender, EventArgs e)
        {
            PopulateRootCombo();
            
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            var keyValue = GetCurrentKeyValue();
            MessageBox.Show($"The current value of the registry key selected is: {keyValue}", @"Current Value of Key",
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
                var newGeneralStruct = new GeneralStruct {RegistryRoot = GetCurrentRoot(), RegistryField = fieldTextBox.Text};
                var dir = new DirectoryUtils();
                dir.SaveGeneralStructToFile(newGeneralStruct);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        #region Populate Combos
        private void PopulateRootCombo()
        {
            rootCombo.Items.Add(Registry.ClassesRoot.Name);
            rootCombo.Items.Add(Registry.CurrentUser.Name);
            rootCombo.Items.Add(Registry.LocalMachine.Name);
            rootCombo.Items.Add(Registry.Users.Name);
            rootCombo.Items.Add(Registry.CurrentConfig.Name);
        }

        private void RootCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo2.Items.Clear();
            rootCombo3.Items.Clear();
            PopulateRootCombo2();
        }

        private void PopulateRootCombo2()
        {
            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            
            foreach (var name in currentRoot.GetSubKeyNames())
            {
                rootCombo2.Items.Add(name);
            }
        }


        private RegistryKey GetRegistryKeyFromText(string key)
        {
            if (key == Registry.ClassesRoot.Name)
                return Registry.ClassesRoot;
            if (key == Registry.CurrentUser.Name)
                return Registry.CurrentUser;
            if (key == Registry.LocalMachine.Name)
                return Registry.LocalMachine;
            if (key == Registry.Users.Name)
                return Registry.Users;
            if (key == Registry.CurrentConfig.Name)
                return Registry.CurrentConfig;
            return Registry.CurrentUser;
        }

        private void RootCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo3.Items.Clear();

            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            var currentSubkey = currentRoot.OpenSubKey(rootCombo2.SelectedItem.ToString());

            foreach (var name in currentSubkey.GetSubKeyNames())
            {
                rootCombo3.Items.Add(name);
            }
        }
        #endregion Populate Combos

        private string GetCurrentKeyValue()
        {
            var userRoot = GetCurrentRoot();

            return (string) Registry.GetValue(userRoot.ToString(), fieldTextBox.Text, "");
        }

        private string GetCurrentRoot()
        {
            var userRoot = new StringBuilder();
            if (rootCombo.SelectedIndex != -1)
                userRoot.Append(rootCombo.SelectedItem);
            if (rootCombo2.SelectedIndex != -1)
                userRoot.Append("\\" + rootCombo2.SelectedItem);
            if (rootCombo3.SelectedIndex != -1)
                userRoot.Append("\\" + rootCombo3.SelectedItem);
            return userRoot.ToString();
        }
    }
}
