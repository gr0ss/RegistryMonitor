using System.IO;
using System.Text;
using System.Windows.Forms;
using gr0ssSysTools.Files;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gr0ssSysTools.FileUtils
{
    public class RegistryKeyUtils
    {
        private const string REGISTRYKEY_FILE_NAME = "registrykey.json";

        public static void WriteRegistryKeySettings(MonitoredRegistryKey monitoredRegistryKey, bool firstLoad = false)
        {
            string environmnentJsonFile = Path.Combine(Directory.GetCurrentDirectory(), REGISTRYKEY_FILE_NAME);
            
            using (StreamWriter file = File.CreateText(environmnentJsonFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                var jsonRegistryKey = JsonConvert.SerializeObject(firstLoad ? GetInitialRegistryKey() : monitoredRegistryKey);
                writer.WriteRaw(jsonRegistryKey);
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

        public static void PopulateComboBoxesBasedOnCurrentRegistryKey(MonitoredRegistryKey registryKey, ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3, TextBox subkeyTextBox)
        {
            var splitRegistryKey = registryKey.Root.Split(char.Parse("\\"));

            PopulateRootCombo(rootCombo);
            rootCombo.SelectedIndex = rootCombo.Items.IndexOf(splitRegistryKey[0]);
            PopulateRootCombo2(rootCombo, rootCombo2);
            rootCombo2.SelectedIndex = rootCombo2.Items.IndexOf(splitRegistryKey[1]);
            PopulateRootCombo3(rootCombo, rootCombo2, rootCombo3);
            rootCombo3.SelectedIndex = rootCombo3.Items.IndexOf(splitRegistryKey[2]);
            subkeyTextBox.Text = registryKey.Subkey;
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
            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            
            foreach (var name in currentRoot.GetSubKeyNames())
            {
                rootCombo2.Items.Add(name);
            }
        }

        public static void PopulateRootCombo3(ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3)
        {
            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            var currentSubkey = currentRoot.OpenSubKey(rootCombo2.SelectedItem.ToString());

            foreach (var name in currentSubkey.GetSubKeyNames())
            {
                rootCombo3.Items.Add(name);
            }
        }

        public static string GetCurrentRoot(ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3)
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
