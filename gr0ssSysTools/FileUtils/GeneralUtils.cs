using System;
using System.IO;
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
            }
            return general;
        }

        public static General GetDefaultGeneralSettings()
        {
            return new General {IconFont = "Arial Narrow", IconFontSize = 7.0f, IconShape = "", ShowBalloonTips = true};
        }
    }
}
