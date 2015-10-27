using System;
using System.IO;
using gr0ssSysTools.Files;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gr0ssSysTools.FileUtils
{
    public class GeneralUtils
    {
        private const string GENERAL_FILE_NAME = "general.txt";
        private const string GENERAL_JSON_FILE_NAME = "registrykey.json";

        public static void WriteGeneralSettingsJson(General general)
        {
            string generalJsonFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_JSON_FILE_NAME);
            
            using (StreamWriter file = File.CreateText(generalJsonFile))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                var jsonGeneralSettings = JsonConvert.SerializeObject(general);
                writer.WriteRaw(jsonGeneralSettings);
            }
        }

        public static General ReadGeneralSettingsJson()
        {
            string generalJsonFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_JSON_FILE_NAME);

            var generalSettings = new General();

            if (!File.Exists(generalJsonFile))
            {
                generalSettings = GetDefaultGeneralSettingsJson();
                WriteGeneralSettingsJson(generalSettings);
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
            }
            return general;
        }

        public static General GetDefaultGeneralSettingsJson()
        {
            return new General {IconFont = "Arial Narrow", IconFontSize = 7.0f, IconShape = "", ShowBalloonTips = true};
        } 

        public static GeneralStruct ReadGeneralStructSettings()
        {
            string generalFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_FILE_NAME);
            if (!File.Exists(generalFile))
            {
                SaveGeneralSettings(new GeneralStruct());
                return new GeneralStruct();
            }

            using (StreamReader sr = new StreamReader(generalFile))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var result = ParseGeneralSetting(line);
                    if (result != null)
                        return result;
                }
            }

            return new GeneralStruct();
        }

        private static GeneralStruct ParseGeneralSetting(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var readLine = input.Split('|');
            
            return new GeneralStruct
            {
                RegistryRoot = readLine[0],
                RegistryField = readLine[1]
            };
        }
        
        public static void SaveGeneralSettings(GeneralStruct generalStruct)
        {
            string generalFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_FILE_NAME);

            using (StreamWriter sw = new StreamWriter(generalFile))
            {
                sw.WriteLine($"{generalStruct.RegistryRoot}|{generalStruct.RegistryField}");
            }
        }
    }
}
