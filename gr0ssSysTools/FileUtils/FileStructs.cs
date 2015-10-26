using System;
using System.Drawing;

namespace gr0ssSysTools.FileUtils
{
    public class FileStruct
    {
        public Guid ID;
        public string Name;
        public string ValueKey;
        public string HotKey;
        public string IconLabel;
        public Brush IconColor;
    }

    public class GeneralStruct
    {
        public string RegistryRoot;
        public string RegistryField;
    }
}
