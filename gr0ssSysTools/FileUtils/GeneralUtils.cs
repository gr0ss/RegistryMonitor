using System;
using System.IO;
using System.Windows.Input;
using gr0ssSysTools.Files;
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
                case Constants.General.ICON_FONT:
                    general.IconFont = propertyValue;
                    break;
                case Constants.General.ICON_FONT_SIZE:
                    general.IconFontSize = Convert.ToSingle(propertyValue);
                    break;
                case Constants.General.ICON_SHAPE:
                    general.IconShape = propertyValue;
                    break;
                case Constants.General.SHOW_BALLOON_TIPS:
                    general.ShowBalloonTips = Convert.ToBoolean(propertyValue);
                    break;
                case Constants.General.LOADED_GLOBAL_HOTKEY:
                    general.LoadedGlobalHotkey = ConvertToLoadedGlobalHotkey(propertyValue);
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

        private static LoadedGlobalHotkey ConvertToLoadedGlobalHotkey(string hotkeyString)
        {
            Key key;
            ModifierKeys key2;
            ModifierKeys key3;
            var hotkeyValues = hotkeyString.Split(char.Parse(","));
            Enum.TryParse(hotkeyValues[0].Split(char.Parse(":"))[1].Replace(" ", ""), out key);
            Enum.TryParse(hotkeyValues[1].Split(char.Parse(":"))[1].Replace(" ", ""), out key2);
            Enum.TryParse(hotkeyValues[2].Split(char.Parse(":"))[1].Replace(" ", "").Replace("\r\n}", ""), out key3);
            return new LoadedGlobalHotkey {Hotkey = key, FirstModifierKey = key2, SecondModifierKey = key3};
        }

        private static LoadedGlobalHotkey GetDefaultLoadedGlobalHotkeySettings()
        {
            return new LoadedGlobalHotkey {Hotkey = Key.Z, FirstModifierKey = ModifierKeys.Windows, SecondModifierKey = ModifierKeys.Alt};
        }
    }
}
