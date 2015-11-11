using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace gr0ssSysTools.FileUtils
{
    public class MiscUtils
    {
        public static string GetNameWithHotkey(string name, string hotkey)
        {
            var positionOfHotkey = name.IndexOf(hotkey, StringComparison.Ordinal);
            return name.Insert(positionOfHotkey, "&");
        }

        public static int GetIndexOfHotkey(string name, string hotkey)
        {
            var indexOfHotkey = name.IndexOf(hotkey, StringComparison.Ordinal);
            var numberOfSpacesBeforeHotkey = name.Substring(0, indexOfHotkey).Count(char.IsSeparator);
            return indexOfHotkey - numberOfSpacesBeforeHotkey;
        }
        
        public static char GetFirstUniqueHotkey(string name, IEnumerable<string> hotkeys)
        {
            var builder = new StringBuilder();

            foreach (var line in hotkeys)
            {
                builder.Append(line.ToUpperInvariant());
                builder.Append(line.ToLowerInvariant());
            }
            
            var charset = builder.ToString().ToCharArray();

            return name.TrimStart(charset)[0];
        }
    }
}
