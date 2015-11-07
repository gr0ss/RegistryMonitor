using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gr0ssSysTools.Parsers
{
    public class FontParser
    {
        public static Font ConvertStringToFont(string fontString)
        {
            var cvt = new FontConverter();
            return cvt.ConvertFromString(fontString + " Regular") as Font;
        }

        public static string ConvertFontToString(Font font)
        {
            var cvt = new FontConverter();
            return cvt.ConvertToString(font);
        }
    }
}
