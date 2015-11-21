using System;

namespace RegistryMonitor.Files
{
    public class LoadedEnvironments
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string SubkeyValue { get; set; }
        public string HotKey { get; set; }
        public string IconLabel { get; set; }
        public string IconTextColor { get; set; }
        public string IconBackgroundColor { get; set; }
        public bool LoadIcon { get; set; }
        public string IconFileLocation { get; set; }
    }
}
