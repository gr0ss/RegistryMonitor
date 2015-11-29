using System.Drawing;
using System.Windows.Media;
using Brush = System.Drawing.Brush;
using Color = System.Drawing.Color;

namespace RegistryMonitor.ExtensionMethods
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Convert string into solid brush.
        /// </summary>
        /// <param name="brushColor"></param>
        /// <returns></returns>
        public static SolidBrush ToSolidBrush(this string brushColor)
        {
            return !string.IsNullOrEmpty(brushColor) 
                ? new SolidBrush(Color.FromName(brushColor)) 
                : null;
        }

        /// <summary>
        /// Convert string into brush.
        /// </summary>
        /// <param name="brushColor"></param>
        /// <returns></returns>
        public static Brush ToBrush(this string brushColor)
        {
            if (!string.IsNullOrEmpty(brushColor))
            {
                return new BrushConverter().ConvertFromInvariantString(brushColor) as Brush;
            }
            return null;
        }

        /// <summary>
        /// Convert solid brush into string.
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        public static string ConvertSolidBrushToString(this SolidBrush brush)
        {
            return brush.Color.ToString();
        }

        /// <summary>
        /// Convert brush into string.
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        public static string ConvertBrushToString(this Brush brush)
        {
            return ((SolidBrush) brush).Color.Name;
        }
    }
}
