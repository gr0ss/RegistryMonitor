using System;
using System.Drawing;

namespace gr0ssSysTools.FileUtils
{
    public struct EnvironmentStruct
    {
        public Guid ID;
        public string Name;
        public string RegistryKey;
        public string HotKey;
    }

    public struct ToolsStruct
    {
        public Guid ID;
        public string Name;
        public string DirectoryPath;
        public string HotKey;
    }

    public struct FileStruct
    {
        public Guid ID;
        public string Name;
        public string ValueKey;
        public string HotKey;
        public string IconLabel;
        public Brush IconColor;
    }
}
