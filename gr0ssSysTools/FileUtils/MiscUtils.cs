using System.Drawing;
using System.Drawing.Imaging;
using ComboxExtended;

namespace gr0ssSysTools.FileUtils
{
    public class MiscUtils
    {
        public string GetNameWithHotkey(string name, string hotkey)
        {
            var positionOfHotkey = name.IndexOf(hotkey);
            return name.Insert(positionOfHotkey, "&");
        }

        public ComboBoxItem CreateComboBoxItem(string color, Brush brushColor)
        {
            var red = new ComboBoxItem();
            red.Value = color;

            Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			using (Graphics g = Graphics.FromImage(bmp))
			{
                Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			    g.FillEllipse(brushColor, rectangle);
			}
            red.Image = bmp;
            return red;
        }

        public int GetColorIndex(Brush brushColor)
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
