using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegistryMonitor.ExtensionMethods;
using RegistryMonitor.Files;
using RegistryMonitor.Structs;
using RegistryMonitor.Utils;

namespace RegistryMonitor.FileUtils
{
    public class RegistryKeyUtils
    {
        private const string REGISTRYKEY_FILE_NAME = "registrykey.json";

        public static void WriteRegistryKeySettings(MonitoredRegistryKey monitoredRegistryKey, bool firstLoad = false)
        {
            string environmnentJsonFile = Path.Combine(Directory.GetCurrentDirectory(), REGISTRYKEY_FILE_NAME);

            try
            {
                using (StreamWriter file = File.CreateText(environmnentJsonFile))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    var jsonRegistryKey = JsonConvert.SerializeObject(firstLoad ? GetInitialRegistryKey() : monitoredRegistryKey);
                    writer.WriteRaw(jsonRegistryKey);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Constants.RegistryKeyMessages.ErrorWritingFile}{ex}", 
                    Constants.RegistryKeyMessages.ErrorWritingFileCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static MonitoredRegistryKey ReadRegistryKeySettings()
        {
            string registryKeyJsonFile = Path.Combine(Directory.GetCurrentDirectory(), REGISTRYKEY_FILE_NAME);

            var registryKey = new MonitoredRegistryKey();

            if (!File.Exists(registryKeyJsonFile))
            {
                registryKey = GetInitialRegistryKey();
                WriteRegistryKeySettings(registryKey);
            }
            else
            {
                try
                {
                    using (StreamReader file = File.OpenText(registryKeyJsonFile))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        while (reader.Read())
                        {
                            JObject o3 = (JObject) JToken.ReadFrom(reader);
                            foreach (var child in o3.Children())
                            {
                                AddPropertyToRegistryKey(registryKey, child.Path, child.First.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{Constants.RegistryKeyMessages.ErrorWritingFile}{ex}", 
                        Constants.RegistryKeyMessages.ErrorWritingFileCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            return registryKey;
        }

        private static MonitoredRegistryKey AddPropertyToRegistryKey(MonitoredRegistryKey monitoredRegistryKey, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case FileConstants.RegistryKey.ROOT:
                    monitoredRegistryKey.Root = propertyValue;
                    break;
                case FileConstants.RegistryKey.SUBKEY:
                    monitoredRegistryKey.Subkey = propertyValue;
                    break;
            }
            return monitoredRegistryKey;
        }

        private static MonitoredRegistryKey GetInitialRegistryKey()
        {
            return new MonitoredRegistryKey {Root = "", Subkey = ""};
        }

        public static RegistryKey GetRegistryKeyFromCombo(ComboBox combo)
        {
            return GetRegistryKeyFromText(!string.IsNullOrEmpty(combo.SelectedText) 
                                          ? combo.SelectedText 
                                          : combo.SelectedItem.ToString());
        }

        public static RegistryKey GetRegistryKeyFromText(string key)
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

        public static void PopulateComboBoxesBasedOnCurrentRegistryKey(MonitoredRegistryKey registryKey, RegistryKeyObjectStruct registryKeyObjectStruct)
        {
            var splitRegistryKey = registryKey.Root.Split(char.Parse("\\"));

            PopulateRootCombo(registryKeyObjectStruct.Root);
            registryKeyObjectStruct.Root.SelectedIndex = registryKeyObjectStruct.Root.Items.GetIndex(splitRegistryKey[0]);
            PopulateRootCombo2(registryKeyObjectStruct.Root, registryKeyObjectStruct.Root2);
            registryKeyObjectStruct.Root2.SelectedIndex = registryKeyObjectStruct.Root2.Items.GetIndex(splitRegistryKey[1]);
            if (registryKeyObjectStruct.Root2.SelectedIndex != -1)
            {
                PopulateRootCombo3(registryKeyObjectStruct.Root, registryKeyObjectStruct.Root2, registryKeyObjectStruct.Root3);
                registryKeyObjectStruct.Root3.SelectedIndex = registryKeyObjectStruct.Root3.Items.GetIndex(splitRegistryKey[2]);
            }
            registryKeyObjectStruct.Subkey.Text = registryKey.Subkey;
        }

        public static void PopulateRootCombo(ComboBox rootCombo)
        {
            rootCombo.Items.Add(Registry.ClassesRoot.Name);
            rootCombo.Items.Add(Registry.CurrentUser.Name);
            rootCombo.Items.Add(Registry.LocalMachine.Name);
            rootCombo.Items.Add(Registry.Users.Name);
            rootCombo.Items.Add(Registry.CurrentConfig.Name);
        }

        public static void PopulateRootCombo2(ComboBox rootCombo, ComboBox rootCombo2)
        {
            var currentRoot = GetRegistryKeyFromCombo(rootCombo);

            if (currentRoot == null) return;
            
            foreach (var name in currentRoot.GetSubKeyNames())
            {
                rootCombo2.Items.Add(name);
            }
        }

        public static void PopulateRootCombo3(ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3)
        {
            var currentRoot = GetRegistryKeyFromCombo(rootCombo);
            var currentSubkey = currentRoot.OpenSubKey(rootCombo2.SelectedItem.ToString());

            if (currentSubkey == null) return;

            foreach (var name in currentSubkey.GetSubKeyNames())
            {
                rootCombo3.Items.Add(name);
            }
        }

        public static string GetCurrentRoot(RegistryKeyStruct registryKey)
        {
            var userRoot = new StringBuilder();
            if (registryKey.Root.SelectedIndex != -1)
                userRoot.Append(registryKey.Root.SelectedItem);
            if (registryKey.Root2.SelectedIndex != -1)
                userRoot.Append("\\" + registryKey.Root2.SelectedItem);
            if (registryKey.Root3.SelectedIndex != -1)
                userRoot.Append("\\" + registryKey.Root3.SelectedItem);
            return userRoot.ToString();
        }

        private static string GetCurrentKeyValue(RegistryKeyStruct registryKey)
        {
            return (string) Registry.GetValue(GetCurrentRoot(registryKey), registryKey.Subkey, "");
        }

        private static bool CurrentKeyEqualsSavedKey(MonitoredRegistryKey monitoredKey, RegistryKeyStruct registryKey)
        {
            var currentRoot = GetCurrentRoot(registryKey);

            return currentRoot == monitoredKey.Root &&
                   registryKey.Subkey == monitoredKey.Subkey;
        }

        public static void SaveNewRegistryKey(LoadedSettings loadedSettings, RegistryKeyStruct registryKey)
        {
            if (GetCurrentKeyValue(registryKey) == string.Empty) // New Key is invalid.
            {
                MessageBox.Show(Constants.RegistryKeyMessages.SelectRegistryKey, 
                                Constants.RegistryKeyMessages.SelectRegistryKeyCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (CurrentKeyEqualsSavedKey(loadedSettings.MonitoredRegistryKey, registryKey)) // New Key is Old Key.
            {
                return;
            }
            else // Save New Key.
            {
                var confirmMessage = MessageBox.Show(Constants.RegistryKeyMessages.OverrideRegistryKey, 
                                                     Constants.RegistryKeyMessages.OverrideRegistryKeyCaption, 
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

                if (confirmMessage != DialogResult.Yes) return;

                var newRegistryKey = new MonitoredRegistryKey
                {
                    Root = GetCurrentRoot(registryKey),
                    Subkey = registryKey.Subkey
                };
                loadedSettings.MonitoredRegistryKey = newRegistryKey;
            }
        }

        public static void CheckCurrentKeyValue(RegistryKeyStruct registryKey)
        {
            var rootValue = GetCurrentRoot(registryKey);
            var keyValue = GetCurrentKeyValue(registryKey);

            MessageBox.Show($"{Constants.RegistryKeyMessages.CurrentSelectedKey}" +
                            $"{rootValue}\\{registryKey.Subkey}" +
                            $"{Constants.RegistryKeyMessages.CurrentValueOfKey}" +
                            $"{keyValue}", 
                            Constants.RegistryKeyMessages.CurrentValueOfKeyCaption, 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
