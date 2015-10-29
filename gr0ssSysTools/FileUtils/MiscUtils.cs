using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static int GetColorIndex(string brushColor)
        {
            switch (brushColor)
            {
                case "DarkGray":
                    return 0;
                case "Blue":
                    return 1;
                case "Brown":
                    return 2;
                case "Coral":
                    return 3;
                case "Red":
                    return 4;
                case "Aqua":
                    return 5;
                case "Violet":
                    return 6;
                case "Green":
                    return 7;
                case "Yellow":
                    return 8;
                case "Magenta":
                    return 9;
                case "Wheat":
                    return 10;
                case "Orange":
                    return 11;
            }
            return -1;
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
