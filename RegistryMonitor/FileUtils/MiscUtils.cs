using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegistryMonitor.FileUtils
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

        public static void PopulateHotkeyCombo(ComboBox currentHotKeyCombo, string currentName)
        {
            currentHotKeyCombo.Items.Clear();
            foreach (var c in currentName.Where(c => c.ToString() != " "))
            {
                currentHotKeyCombo.Items.Add(c);
            }
        }
    }
}
