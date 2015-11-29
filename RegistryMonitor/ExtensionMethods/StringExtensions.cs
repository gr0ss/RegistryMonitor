using System;
using System.Globalization;
using System.Windows.Forms;

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
            return !string.IsNullOrEmpty(str) 
                ? float.Parse(str, CultureInfo.InvariantCulture.NumberFormat) 
                : float.NaN;
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
            if (!string.IsNullOrEmpty(str))
            {
                return str.IndexOf(compareTo, comp) >= 0;
            }
            return false;
        }

        /// <summary>
        /// Checks the object collection for a string ignoreing the case.
        /// </summary>
        /// <param name="ary"></param>
        /// <param name="stringToFind"></param>
        /// <returns></returns>
        public static int GetIndex(this ComboBox.ObjectCollection ary, string stringToFind)
        {
            if (ary.Count <= 0) return -1;

            for (int i = 0; i < ary.Count; i++)
            {
                if (ary[i].ToString().Equals(stringToFind, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
