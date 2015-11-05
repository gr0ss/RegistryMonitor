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
    }
}
