using System.Drawing;

namespace gr0ssSysTools.Parsers
{
    public class ColorParser
    {
        public static SolidBrush ConvertStringToSolidBrush(string brushColor)
        {
            return new SolidBrush(Color.FromName(brushColor));
        }

        public static string ConvertSolidBrushToString(SolidBrush brush)
        {
            return brush.Color.ToString();
        }

        public static string ConvertBrushToString(Brush brush)
        {
            return ((SolidBrush) brush).Color.ToString();
        }
        
        public static string[] GetAllColors()
        {
            return new[]
            {
                "Dark Gray",
                "Blue",
                "Brown",
                "Coral",
                "Red",
                "Aqua",
                "Violet",
                "Green",
                "Yellow",
                "Magenta",
                "Wheat",
                "Orange"
            };
        }
    }
}
