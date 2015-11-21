using System;
using System.Windows.Forms;
using RegistryMonitor.ExtensionMethods;

namespace RegistryMonitor.Utils
{
    public class ColorUtils
    {
        public static string[] GetAllColors()
        {
            return new[]
            {
                "Dark Gray",
                "Blue",
                "Black",
                "Brown",
                "Coral",
                "Red",
                "Aqua",
                "Violet",
                "Green",
                "Yellow",
                "Magenta",
                "Wheat",
                "White",
                "Orange"
            };
        }

        public static void PopulateColorComboBox(string currentColor, ComboBox colorComboBox)
        {
            colorComboBox.Items.Clear();
            foreach (var color in GetAllColors())
            {
                colorComboBox.Items.Add(new ColorDropDownItem(color, color.ToSolidBrush()));
            }

            var colorIndex = Array.FindIndex(ColorUtils.GetAllColors(), row => row.Contains(currentColor));
            colorComboBox.SelectedItem = colorComboBox.Items[colorIndex];
        }
    }
}
