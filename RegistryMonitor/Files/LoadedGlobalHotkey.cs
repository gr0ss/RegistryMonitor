using System.Windows.Input;

namespace RegistryMonitor.Files
{
    public class LoadedGlobalHotkey
    {
        public Key Hotkey { get; set; }
        public ModifierKeys FirstModifierKey { get; set; }
        public ModifierKeys SecondModifierKey { get; set; }
    }
}
