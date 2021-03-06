﻿using System;
using System.Windows.Forms;
using Microsoft.Win32;
using RegistryMonitor.FileUtils;
using RegistryMonitor.Structs;
using RegistryMonitor.Utils;

namespace RegistryMonitor
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
            var rootValue = RegistryKeyUtils.GetCurrentRoot(CreateRegistryKeyStruct());

            MessageBox.Show($"{Constants.RegistryKeyMessages.CurrentSelectedKey}" +
                            $"{rootValue}\\{fieldTextBox.Text}" +
                            $"{Constants.RegistryKeyMessages.CurrentValueOfKey}" +
                            $"{GetCurrentKeyValue()}", 
                            Constants.RegistryKeyMessages.CurrentValueOfKeyCaption, 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var keyValue = GetCurrentKeyValue();

            if (keyValue == string.Empty)
            {
                MessageBox.Show(Constants.RegistryKeyMessages.SelectRegistryKey, 
                                Constants.RegistryKeyMessages.SelectRegistryKeyCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var newRegistryKey = new Files.MonitoredRegistryKey
                {
                    Root = RegistryKeyUtils.GetCurrentRoot(CreateRegistryKeyStruct()),
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
            return (string) Registry.GetValue(RegistryKeyUtils.GetCurrentRoot(CreateRegistryKeyStruct()), fieldTextBox.Text, "");
        }

        private RegistryKeyStruct CreateRegistryKeyStruct()
        {
            return new RegistryKeyStruct
            {
                Root = rootCombo,
                Root2 = rootCombo2,
                Root3 = rootCombo3,
                Subkey = fieldTextBox.Text
            };
        }
    }
}
