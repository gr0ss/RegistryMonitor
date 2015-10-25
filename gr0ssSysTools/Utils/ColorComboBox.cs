using System;
using System.Drawing;
using System.Windows.Forms;

namespace gr0ssSysTools.Utils
{
    class ColorComboBox : ComboBox
    {
        public ColorComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Draws the items into the ColorSelector object
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index != -1) {
                var item = Items[e.Index] as ColorDropDownItem;
                if (item != null) {
                    // Draw the colored 16 x 16 square
                    e.Graphics.DrawImage(item.Image, e.Bounds.Left, e.Bounds.Top);
                    // Draw the value (in this case, the color name)
                    e.Graphics.DrawString(item.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Image.Width, e.Bounds.Top + 2);
                }
            }

            base.OnDrawItem(e);
        }
    }

    public class ColorDropDownItem
    {   
        public ColorDropDownItem(string value, Brush brush)
        {
            Value = value;

            Image = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(Image);

            g.DrawRectangle(Pens.White, 0, 0, Image.Width, Image.Height);
            g.FillRectangle(brush, 1, 1, Image.Width - 1, Image.Height - 1);
        }
        
        public Image Image { get; private set; }
        public string Value { get; private set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
