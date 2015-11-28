using System;
using System.Windows.Forms;
using System.Windows.Input;
using GlobalHotKey;
using RegistryMonitor.ExtensionMethods;
using RegistryMonitor.Files;
using RegistryMonitor.Parsers;
using RegistryMonitor.Utils;

namespace RegistryMonitor.FileUtils
{
    public class GlobalHotkeyUtils
    {
        public static void RegisterGlobalHotkey(HotKeyManager hkManager, LoadedGlobalHotkey hotkey)
        {
            if (hotkey.SecondModifierKey != ModifierKeys.None)
            {
                hkManager.Register(hotkey.Hotkey, hotkey.FirstModifierKey | hotkey.SecondModifierKey);
            }
            else
            {
                hkManager.Register(hotkey.Hotkey, hotkey.FirstModifierKey);
            }
        }

        public static void UnregisterGlobalHotkey(HotKeyManager hkManager, LoadedGlobalHotkey hotkey)
        {
            if (hotkey.SecondModifierKey != ModifierKeys.None)
            {
                hkManager.Unregister(hotkey.Hotkey, hotkey.FirstModifierKey | hotkey.SecondModifierKey);
            }
            else
            {
                hkManager.Unregister(hotkey.Hotkey, hotkey.FirstModifierKey);
            }
        }

        public static void PopulateGlobalHotkeyCombos(LoadedGlobalHotkey loadedHotkey, ComboBox hotkeyCombo, ComboBox firstModifierKeyCombo, ComboBox secondModifierKeyCombo)
        {
            var keyNames = Enum.GetNames(typeof (Key));
            var modifierKeyNames = Enum.GetNames(typeof (ModifierKeys));
            
            foreach (var keyName in keyNames)
            {
                hotkeyCombo.Items.Add(keyName);
            }

            foreach (var modifierKeyName in modifierKeyNames)
            {
                firstModifierKeyCombo.Items.Add(modifierKeyName);
                secondModifierKeyCombo.Items.Add(modifierKeyName);
            }

            hotkeyCombo.SelectedIndex = hotkeyCombo.Items.GetIndex(loadedHotkey.Hotkey.ToString());
            firstModifierKeyCombo.SelectedIndex = firstModifierKeyCombo.Items.GetIndex(loadedHotkey.FirstModifierKey.ToString());
            secondModifierKeyCombo.SelectedIndex = secondModifierKeyCombo.Items.GetIndex(loadedHotkey.SecondModifierKey.ToString());
        }

        public static void SaveNewGlobalHotkey(LoadedSettings loadedSettings, string globalHotKey, string firstModifierKey, string secondModifierKey)
        {
            if (firstModifierKey == ModifierKeys.None.ToString())
            {
                MessageBox.Show(Constants.HotkeyMessages.SelectGlobalHotkeyToSave, 
                                Constants.HotkeyMessages.SelectGlobalHotkeyToSaveCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else if (CurrentHotkeyEqualsSavedHotkey(loadedSettings, globalHotKey, firstModifierKey, secondModifierKey))
            {
                return;
            }
            else
            {
                loadedSettings.General.LoadedGlobalHotkey.Hotkey =
                    GlobalHotkeyParser.ConvertStringToKey(globalHotKey);
                loadedSettings.General.LoadedGlobalHotkey.FirstModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(firstModifierKey);
                loadedSettings.General.LoadedGlobalHotkey.SecondModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(secondModifierKey);
            }
        }

        private static bool CurrentHotkeyEqualsSavedHotkey(LoadedSettings loadedSettings, string globalHotKey, string firstModifierKey, string secondModifierKey)
        {
            return loadedSettings.General.LoadedGlobalHotkey.Hotkey.ToString() == globalHotKey &&
                   loadedSettings.General.LoadedGlobalHotkey.FirstModifierKey.ToString() == firstModifierKey &&
                   loadedSettings.General.LoadedGlobalHotkey.SecondModifierKey.ToString() == secondModifierKey;
        }

        public static void SetNewGlobalHotkeyIfChanged(LoadedSettings loadedSettings, LoadedGlobalHotkey loadedGlobalHotkey, HotKeyManager hkManager)
        {
            if (loadedGlobalHotkey == loadedSettings.General.LoadedGlobalHotkey) return;

            UnregisterGlobalHotkey(hkManager, loadedGlobalHotkey);
            RegisterGlobalHotkey(hkManager, loadedSettings.General.LoadedGlobalHotkey);
            loadedGlobalHotkey = loadedSettings.General.LoadedGlobalHotkey;
        }
    }
}
