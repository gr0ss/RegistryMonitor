using System.Windows.Forms;

namespace RegistryMonitor.Structs
{
    public struct RegistryKeyStruct
    {
        public ComboBox Root;
        public ComboBox Root2;
        public ComboBox Root3;
        public string Subkey;
    }

    public struct RegistryKeyObjectStruct
    {
        public ComboBox Root;
        public ComboBox Root2;
        public ComboBox Root3;
        public TextBox Subkey;
    }
}
