using System;
using System.Windows.Input;
using gr0ssSysTools.Files;

namespace gr0ssSysTools.Parsers
{
    public class GlobalHotkeyParser
    {
        public static Key ConvertStringToKey(string keyToConvert)
        {
            Key key;
            Enum.TryParse(keyToConvert, out key);
            return key;
        }

        public static ModifierKeys ConvertStringToModifierKeys(string keyToConvert)
        {
            ModifierKeys key;
            Enum.TryParse(keyToConvert, out key);
            return key;
        }

        public static LoadedGlobalHotkey ConvertToLoadedGlobalHotkey(string hotkeyString)
        {
            var hotkeyValues = hotkeyString.Replace(" ", "")
                                           .Replace("\r\n}", "")
                                           .Split(char.Parse(","));

            return new LoadedGlobalHotkey
            {
                Hotkey = ConvertStringToKey(hotkeyValues[0].Split(char.Parse(":"))[1]),
                FirstModifierKey = ConvertStringToModifierKeys(hotkeyValues[1].Split(char.Parse(":"))[1]),
                SecondModifierKey = ConvertStringToModifierKeys(hotkeyValues[2].Split(char.Parse(":"))[1])
            };
        }
    }
}
