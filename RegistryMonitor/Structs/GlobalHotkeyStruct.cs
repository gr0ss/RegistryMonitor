using System.Windows.Forms;
using System.Windows.Input;

namespace RegistryMonitor.Structs
{
    public struct GlobalHotkeyStruct
    {
        public string Hotkey;
        public string FirstModifierKey;
        public string SecondModifierKey;
    }

    public struct GlobalHotkeyObjectStruct
    {
        public ComboBox Hotkey;
        public ComboBox FirstModifierKey;
        public ComboBox SecondModifierKey;
    }
}
