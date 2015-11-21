using System;
using System.Globalization;

namespace RegistryMonitor.ExtensionMethods
{
    public static class StringExtensions
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

        /// <summary>
        /// Checks if one string contains another with being able to ignore case.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="compareTo"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string str, string compareTo, StringComparison comp)
        {
            return str.IndexOf(compareTo, comp) >= 0;
        }
    }
}
