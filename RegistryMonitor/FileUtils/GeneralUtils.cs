﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using RegistryMonitor.ExtensionMethods;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegistryMonitor.Files;
using RegistryMonitor.Parsers;
using RegistryMonitor.Structs;
using RegistryMonitor.Utils;

namespace RegistryMonitor.FileUtils
{
    public class GeneralUtils
    {
        private const string GENERAL_FILE_NAME = "general.json";

        public static void WriteGeneralSettings(General general)
        {
            string generalJsonFile = Path.Combine(Directory.GetCurrentDirectory(), GENERAL_FILE_NAME);

            try
            {
                using (StreamWriter file = File.CreateText(generalJsonFile))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    var jsonGeneralSettings = JsonConvert.SerializeObject(general);
                    writer.WriteRaw(jsonGeneralSettings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Constants.GeneralMessages.ErrorWritingFile}{ex}", 
                    Constants.GeneralMessages.ErrorWritingFileCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
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
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show($"{Constants.GeneralMessages.ErrorWritingFile}{ex}", 
                        Constants.GeneralMessages.ErrorWritingFileCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
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

        public static void SaveGeneralSettings(LoadedSettings loadedSettings, GeneralStruct general)
        {
            RegistryKeyUtils.SaveNewRegistryKey(loadedSettings, general.RegistryKey);
            GlobalHotkeyUtils.SaveNewGlobalHotkey(loadedSettings, general.GlobalHotkey);
            loadedSettings.General.ShowBalloonTips = general.ShowBalloonTips;
            loadedSettings.General.IconFont = general.IconFont;
            loadedSettings.General.IconFontSize = general.IconFontSize;
            loadedSettings.General = loadedSettings.General;

            MessageBox.Show($"Settings {Constants.Messages.SavedSuccessfully}",
                            $"Settings {Constants.Messages.SavedSuccessfullyCaption}",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
