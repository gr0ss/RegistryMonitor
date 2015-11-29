using System;

namespace RegistryMonitor.Structs
{
    public struct EnvironmentStruct
    {
        public Guid ID;
        public string Name;
        public string SubkeyValue;
        public string HotKey;
        public string IconLabel;
        public string IconTextColor;
        public string IconBackgroundColor;
        public bool LoadIcon;
        public string IconFileLocation;
        public bool DisplayOnMenu;
    }
}
