using System;

namespace RegistryMonitor.Files
{
    public class LoadedTools
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string FileLocation { get; set; }
        public string HotKey { get; set; }
    }
}
