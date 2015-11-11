using System.Globalization;

namespace gr0ssSysTools.ExtensionMethods
{
    public static class NumericExtensions
    {
        /// <summary>
        /// Converts string into float.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(this string str)
        {
            return float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}
