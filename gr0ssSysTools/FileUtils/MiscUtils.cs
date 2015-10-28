using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using gr0ssSysTools.Files;

namespace gr0ssSysTools.FileUtils
{
    public class MiscUtils
    {
        public static string GetNameWithHotkey(string name, string hotkey)
        {
            var positionOfHotkey = name.IndexOf(hotkey);
            return name.Insert(positionOfHotkey, "&");
        }

        public static int GetIndexOfHotkey(string name, string hotkey)
        {
            var indexOfHotkey = name.IndexOf(hotkey, StringComparison.Ordinal);
            var numberOfSpacesBeforeHotkey = name.Substring(0, indexOfHotkey).Count(char.IsSeparator);
            return indexOfHotkey - numberOfSpacesBeforeHotkey;
        }

        public static char GetFirstUniqueHotkey(string name, params char[] charset)
        {
            return name.TrimStart(charset)[0];
        }

        public static char[] GetAllEnvironmentsHotkeys(IEnumerable<Environments> environments)
        {
            var builder = new StringBuilder();
            foreach (var line in environments)
            {
                builder.Append(line.HotKey.ToUpperInvariant());
                builder.Append(line.HotKey.ToLowerInvariant());
            }
            return builder.ToString().ToCharArray();
        }

        public static char[] GetAllToolsHotkeys(IEnumerable<Tools> tools)
        {
            var builder = new StringBuilder();
            foreach (var line in tools)
            {
                builder.Append(line.HotKey.ToUpperInvariant());
                builder.Append(line.HotKey.ToLowerInvariant());
            }
            return builder.ToString().ToCharArray();
        }
        
        public static int GetColorIndex(Brush brushColor)
        {
            if (((SolidBrush)brushColor).Color == Color.DarkGray)
                return 0;
            if (((SolidBrush)brushColor).Color == Color.Blue)
                return 1;
            if (((SolidBrush)brushColor).Color == Color.Brown)
                return 2;
            if (((SolidBrush)brushColor).Color == Color.Coral)
                return 3;
            if (((SolidBrush)brushColor).Color == Color.Red)
                return 4;
            if (((SolidBrush)brushColor).Color == Color.Aqua)
                return 5;
            if (((SolidBrush)brushColor).Color == Color.Violet)
                return 6;
            if (((SolidBrush)brushColor).Color == Color.Green)
                return 7;
            if (((SolidBrush)brushColor).Color == Color.Yellow)
                return 8;
            if (((SolidBrush)brushColor).Color == Color.Magenta)
                return 9;
            if (((SolidBrush)brushColor).Color == Color.Wheat)
                return 10;
            if (((SolidBrush)brushColor).Color == Color.Orange)
                return 11;

            return -1;
        }
    }
}
