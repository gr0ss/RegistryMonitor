using System.Windows.Input;

namespace gr0ssSysTools.Files
{
    public class LoadedGlobalHotkey
    {
        public Key Hotkey { get; set; }
        public ModifierKeys FirstModifierKey { get; set; }
        public ModifierKeys SecondModifierKey { get; set; }
    }
}
