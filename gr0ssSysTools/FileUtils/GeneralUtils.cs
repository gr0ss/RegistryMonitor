using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using gr0ssSysTools.ExtensionMethods;
using gr0ssSysTools.Files;
using gr0ssSysTools.Parsers;
using gr0ssSysTools.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gr0ssSysTools.FileUtils
{
    public class GeneralUtils
    {
        private const string GENERAL_FILE_NAME = "general.json";

        public static void WriteGeneralSettings(General general)
        {
            string generalJsonFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_FILE_NAME);
            
            using (StreamWriter file = File.CreateText(generalJsonFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                var jsonGeneralSettings = JsonConvert.SerializeObject(general);
                writer.WriteRaw(jsonGeneralSettings);
            }
        }

        public static General ReadGeneralSettings()
        {
            string generalJsonFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_FILE_NAME);

            var generalSettings = new General();

            if (!File.Exists(generalJsonFile))
            {
                generalSettings = GetDefaultGeneralSettings();
                WriteGeneralSettings(generalSettings);
            }
            else
            {
                using (StreamReader file = File.OpenText(generalJsonFile))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    while (reader.Read())
                    {
                        JObject o3 = (JObject) JToken.ReadFrom(reader);
                        foreach (var child in o3.Children())
                        {
                            AddPropertyToGeneralSettings(generalSettings, child.Path, child.First.ToString());
                        }
                    }
                }
            }
            return generalSettings;
        }

        private static General AddPropertyToGeneralSettings(General general, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case FileConstants.General.ICON_FONT:
                    general.IconFont = propertyValue;
                    break;
                case FileConstants.General.ICON_FONT_SIZE:
                    general.IconFontSize = propertyValue.ToFloat();
                    break;
                case FileConstants.General.ICON_SHAPE:
                    general.IconShape = propertyValue;
                    break;
                case FileConstants.General.SHOW_BALLOON_TIPS:
                    general.ShowBalloonTips = Convert.ToBoolean(propertyValue);
                    break;
                case FileConstants.General.LOADED_GLOBAL_HOTKEY:
                    general.LoadedGlobalHotkey = GlobalHotkeyParser.ConvertToLoadedGlobalHotkey(propertyValue);
                    break;
            }
            return general;
        }

        public static General GetDefaultGeneralSettings()
        {
            return new General
            {
                IconFont = "Arial Narrow",
                IconFontSize = 7.0f,
                IconShape = "",
                ShowBalloonTips = true,
                LoadedGlobalHotkey = GetDefaultLoadedGlobalHotkeySettings()
            };
        }
        
        private static LoadedGlobalHotkey GetDefaultLoadedGlobalHotkeySettings()
        {
            return new LoadedGlobalHotkey {Hotkey = Key.Z, FirstModifierKey = ModifierKeys.Windows, SecondModifierKey = ModifierKeys.Alt};
        }

        public static void PopulateIconProperties(General generalSettings, ComboBox fontComboBox, ComboBox colorComboBox, ComboBox textColorComboBox)
        {
            PopulateFontComboBox(generalSettings.IconFont, fontComboBox);
            ColorUtils.PopulateColorComboBox("Blue", colorComboBox);
            ColorUtils.PopulateColorComboBox("White", textColorComboBox);
        }

        private static void PopulateFontComboBox(string currentFont, ComboBox fontComboBox)
        {
            var listOfFonts = FontFamily.Families;

            foreach (var fontFamily in listOfFonts)
            {
                fontComboBox.Items.Add(fontFamily.Name);
            }
            
            fontComboBox.SelectedItem = currentFont;
        }
    }
}
