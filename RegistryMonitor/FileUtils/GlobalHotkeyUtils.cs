using System;
using System.Windows.Forms;
using System.Windows.Input;
using GlobalHotKey;
using RegistryMonitor.ExtensionMethods;
using RegistryMonitor.Files;
using RegistryMonitor.Parsers;
using RegistryMonitor.Structs;
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

        public static void PopulateGlobalHotkeyCombos(LoadedGlobalHotkey loadedHotkey, GlobalHotkeyObjectStruct globalHotkey)
        {
            var keyNames = Enum.GetNames(typeof (Key));
            var modifierKeyNames = Enum.GetNames(typeof (ModifierKeys));
            
            foreach (var keyName in keyNames)
            {
                globalHotkey.Hotkey.Items.Add(keyName);
            }

            foreach (var modifierKeyName in modifierKeyNames)
            {
                globalHotkey.FirstModifierKey.Items.Add(modifierKeyName);
                globalHotkey.SecondModifierKey.Items.Add(modifierKeyName);
            }

            globalHotkey.Hotkey.SelectedIndex = globalHotkey.Hotkey.Items.GetIndex(loadedHotkey.Hotkey.ToString());
            globalHotkey.FirstModifierKey.SelectedIndex = globalHotkey.FirstModifierKey.Items.GetIndex(loadedHotkey.FirstModifierKey.ToString());
            globalHotkey.SecondModifierKey.SelectedIndex = globalHotkey.SecondModifierKey.Items.GetIndex(loadedHotkey.SecondModifierKey.ToString());
        }

        public static void SaveNewGlobalHotkey(LoadedSettings loadedSettings, GlobalHotkeyStruct globalHotkey)
        {
            if (globalHotkey.FirstModifierKey == ModifierKeys.None.ToString())
            {
                MessageBox.Show(Constants.HotkeyMessages.SelectGlobalHotkeyToSave, 
                                Constants.HotkeyMessages.SelectGlobalHotkeyToSaveCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else if (CurrentHotkeyEqualsSavedHotkey(loadedSettings, globalHotkey.Hotkey, globalHotkey.FirstModifierKey, globalHotkey.SecondModifierKey))
            {
                return;
            }
            else
            {
                loadedSettings.General.LoadedGlobalHotkey.Hotkey =
                    GlobalHotkeyParser.ConvertStringToKey(globalHotkey.Hotkey);
                loadedSettings.General.LoadedGlobalHotkey.FirstModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(globalHotkey.FirstModifierKey);
                loadedSettings.General.LoadedGlobalHotkey.SecondModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(globalHotkey.SecondModifierKey);
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
