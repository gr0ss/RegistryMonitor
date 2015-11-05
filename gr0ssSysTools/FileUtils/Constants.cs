using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gr0ssSysTools.FileUtils
{
    public class Constants
    {
        public static class Environments
        {
            public const string ID = "ID";
            public const string NAME = "Name";
            public const string SUBKEY_VALUE = "SubkeyValue";
            public const string HOTKEY = "HotKey";
            public const string ICON_LABEL = "IconLabel";
            public const string ICON_COLOR = "IconColor";
        }

        public static class Tools
        {
            public const string ID = "ID";
            public const string NAME = "Name";
            public const string FILE_LOCATION = "FileLocation";
            public const string HOTKEY = "HotKey";
        }

        public static class RegistryKey
        {
            public const string ROOT = "Root";
            public const string SUBKEY = "Subkey";
        }

        public static class General
        {
            public const string ICON_FONT = "IconFont";
            public const string ICON_FONT_SIZE = "IconFontSize";
            public const string ICON_SHAPE = "IconShape";
            public const string SHOW_BALLOON_TIPS = "ShowBalloonTips";
            public const string LOADED_GLOBAL_HOTKEY = "LoadedGlobalHotkey";
        }
    }
}
