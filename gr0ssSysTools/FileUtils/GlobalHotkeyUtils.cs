using System;
using System.Windows.Forms;
using System.Windows.Input;
using gr0ssSysTools.Files;
using GlobalHotKey;

namespace gr0ssSysTools.FileUtils
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

            hotkeyCombo.SelectedIndex = hotkeyCombo.Items.IndexOf(loadedHotkey.Hotkey.ToString());
            firstModifierKeyCombo.SelectedIndex = firstModifierKeyCombo.Items.IndexOf(loadedHotkey.FirstModifierKey.ToString());
            secondModifierKeyCombo.SelectedIndex = secondModifierKeyCombo.Items.IndexOf(loadedHotkey.SecondModifierKey.ToString());
        }
    }
}
