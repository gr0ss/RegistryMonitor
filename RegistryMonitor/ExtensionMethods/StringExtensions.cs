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

        /// <summary>
        /// Checks the object collection for a string ignoreing the case.
        /// </summary>
        /// <param name="ary"></param>
        /// <param name="stringToFind"></param>
        /// <returns></returns>
        public static int GetIndex(this ComboBox.ObjectCollection ary, string stringToFind)
        {
            for (int i = 0; i < ary.Count; i++)
            {
                if (ary[i].ToString().Equals(stringToFind, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }
    }
}
