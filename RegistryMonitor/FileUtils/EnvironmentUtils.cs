using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegistryMonitor.Files;
using RegistryMonitor.Utils;

namespace RegistryMonitor.FileUtils
{
    public class EnvironmentUtils
    {
        private const string ENVIRONMENT_FILE_NAME = "environments.json";

        public static void WriteEnvironmentSettings(IEnumerable<LoadedEnvironments> environments)
        {
            string environmnentJsonFile = Path.Combine(Directory.GetCurrentDirectory(), ENVIRONMENT_FILE_NAME);

            try
            {
                using (StreamWriter file = File.CreateText(environmnentJsonFile))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    foreach (var environment in environments)
                    {
                        var jsonEnvironment = JsonConvert.SerializeObject(environment);
                        writer.WriteRaw(jsonEnvironment);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Constants.EnvironmentMessages.ErrorWritingFile}{ex}", 
                    Constants.EnvironmentMessages.ErrorWritingFileCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static List<LoadedEnvironments> ReadEnvironmentsSettings()
        {
            var environments = new List<LoadedEnvironments>();

            string environmnentJsonFile = Path.Combine(Directory.GetCurrentDirectory(), ENVIRONMENT_FILE_NAME);

            if (!File.Exists(environmnentJsonFile))
            {
                environments = GetGenericEnvironments();
                WriteEnvironmentSettings(environments);
            }
            else
            {
                try
                {
                    using (StreamReader file = File.OpenText(environmnentJsonFile))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        var env = new LoadedEnvironments();

                        reader.SupportMultipleContent = true;

                        while (reader.Read())
                        {
                            JObject o3 = (JObject) JToken.ReadFrom(reader);
                            foreach (var child in o3.Children())
                            {
                                AddPropertyToEnvironment(env, child.Path, child.First.ToString());
                            }
                            environments.Add(env);
                            env = new LoadedEnvironments();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{Constants.EnvironmentMessages.ErrorReadingFile}{ex}", 
                    Constants.EnvironmentMessages.ErrorReadingFileCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            return environments;
        }

        private static LoadedEnvironments AddPropertyToEnvironment(LoadedEnvironments loadedEnvironment, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case FileConstants.Environments.ID:
                    loadedEnvironment.ID = Guid.Parse(propertyValue);
                    break;
                case FileConstants.Environments.NAME:
                    loadedEnvironment.Name = propertyValue;
                    break;
                case FileConstants.Environments.SUBKEY_VALUE:
                    loadedEnvironment.SubkeyValue = propertyValue;
                    break;
                case FileConstants.Environments.HOTKEY:
                    loadedEnvironment.HotKey = propertyValue;
                    break;
                case FileConstants.Environments.ICON_LABEL:
                    loadedEnvironment.IconLabel = propertyValue;
                    break;
                case FileConstants.Environments.ICON_TEXT_COLOR:
                    loadedEnvironment.IconTextColor = propertyValue;
                    break;
                case FileConstants.Environments.ICON_BACKGROUND_COLOR:
                    loadedEnvironment.IconBackgroundColor = propertyValue;
                    break;
                case FileConstants.Environments.LOAD_ICON:
                    loadedEnvironment.LoadIcon = bool.Parse(propertyValue);
                    break;
                case FileConstants.Environments.ICON_FILE_LOCATION:
                    loadedEnvironment.IconFileLocation = propertyValue;
                    break;
                case FileConstants.Environments.DISPLAY_ON_MENU:
                    loadedEnvironment.DisplayOnMenu = bool.Parse(propertyValue);
                    break;
            }
            return loadedEnvironment;
        }

        public static List<LoadedEnvironments> GetGenericEnvironments()
        {
            return new List<LoadedEnvironments>
            {
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "Development", SubkeyValue = "Data\\devDB.xml", HotKey = "D", IconLabel = "dev", IconTextColor = "White", IconBackgroundColor = "Blue", LoadIcon = false, IconFileLocation = "", DisplayOnMenu = true},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "sb1", SubkeyValue = "Data\\sb1DB.xml", HotKey = "1", IconLabel = "sb1", IconTextColor = "White", IconBackgroundColor = "Blue", LoadIcon = false, IconFileLocation = "", DisplayOnMenu = true},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "sb2", SubkeyValue = "Data\\sb2DB.xml", HotKey = "2", IconLabel = "sb2", IconTextColor = "White", IconBackgroundColor = "Blue", LoadIcon = false, IconFileLocation = "", DisplayOnMenu = true},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "sb3", SubkeyValue = "Data\\sb3DB.xml", HotKey = "3", IconLabel = "sb3", IconTextColor = "White", IconBackgroundColor = "Blue", LoadIcon = false, IconFileLocation = "", DisplayOnMenu = true},
                new LoadedEnvironments {ID = Guid.NewGuid(), Name = "Production", SubkeyValue = "Data\\prdDB.xml", HotKey = "P", IconLabel = "prd", IconTextColor = "White", IconBackgroundColor = "Red", LoadIcon = false, IconFileLocation = "", DisplayOnMenu = true}
            };
        }

        public static void SaveCurrentEnvironment(LoadedSettings loadedSettings, ListBox environmentsListBox, string currentEnvironmentGuid,
            string name, string registryValue, string hotkey, string iconDisplayText, string iconTextColor, string iconBackgroundColor,
            bool iconFromFile, string iconFileLocation, bool displayOnMenu)
        {
            var currentEnvironment = loadedSettings.Environments.First(env => env.ID == Guid.Parse(currentEnvironmentGuid));

            if (string.IsNullOrEmpty(currentEnvironmentGuid)) return;

            if (currentEnvironment.Name != name)
                currentEnvironment.Name = name;
            if (currentEnvironment.SubkeyValue != registryValue)
                currentEnvironment.SubkeyValue = registryValue;
            if (currentEnvironment.HotKey != hotkey)
                currentEnvironment.HotKey = hotkey;
            if (currentEnvironment.IconLabel != iconDisplayText)
                currentEnvironment.IconLabel = iconDisplayText;
            if (currentEnvironment.IconTextColor != iconTextColor)
                currentEnvironment.IconTextColor = iconTextColor;
            if (currentEnvironment.IconBackgroundColor != iconBackgroundColor)
                currentEnvironment.IconBackgroundColor = iconBackgroundColor;
            if (currentEnvironment.LoadIcon != iconFromFile)
                currentEnvironment.LoadIcon = iconFromFile;
            if (currentEnvironment.IconFileLocation != iconFileLocation)
                currentEnvironment.IconFileLocation = iconFileLocation;
            if (currentEnvironment.DisplayOnMenu != displayOnMenu)
                currentEnvironment.DisplayOnMenu = displayOnMenu;

            ListboxUtils.RepopulateListBox(true, environmentsListBox, loadedSettings, currentEnvironmentGuid);
            ListboxUtils.SetCurrentOrderFromListBoxAndSave(true, environmentsListBox, loadedSettings);

            MessageBox.Show($"{currentEnvironment.Name} {Constants.Messages.SavedSuccessfully}",
                            $"Environment {Constants.Messages.SavedSuccessfullyCaption}",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
