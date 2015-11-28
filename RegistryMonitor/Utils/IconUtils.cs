using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FlimFlan.IconEncoder;
using RegistryMonitor.ExtensionMethods;
using RegistryMonitor.Files;
using RegistryMonitor.Properties;

namespace RegistryMonitor.Utils
{
    public static class IconUtils
    {
        public static void UpdateSampleIcon(ComboBox iconFont, ComboBox iconColor, ComboBox iconTextColor, string iconSize, string iconSampleText, PictureBox iconSample)
        {
            // Make sure all properties are filled in
            if (iconFont.SelectedIndex == -1 ||
                iconColor.SelectedIndex == -1 ||
                iconTextColor.SelectedIndex == -1 ||
                string.IsNullOrEmpty(iconSize) ||
                string.IsNullOrEmpty(iconSampleText)) return;

            var size = iconSize.ToFloat();
            if (size <= float.Epsilon) return;

            Font font = new Font(iconFont.SelectedItem.ToString(), size);
            Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			using (Graphics g = Graphics.FromImage(bmp))
			{
                Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			    g.FillEllipse(iconColor.SelectedItem.ToString().ToSolidBrush(), rectangle);
                g.DrawString(iconSampleText, font, iconTextColor.SelectedItem.ToString().ToSolidBrush(), 0, 2);
			}

            iconSample.Image = Converter.BitmapToIcon(bmp).ToBitmap();
        }

        public static void UpdateLoadedSampleIcon(string fileLocation, PictureBox iconSample)
        {
            if (!File.Exists(fileLocation) ||
                !fileLocation.Contains(Constants.FileExtensions.IconExtension, StringComparison.OrdinalIgnoreCase))
            {
                iconSample.Image = null;
                return;
            }
            
            try
            {
                var iconFromFile = new Icon(fileLocation, 16, 16);
                iconSample.Image = iconFromFile.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.IconMessages.ErrorLoadingIcon + ex, 
                                Constants.IconMessages.ErrorLoadingIconCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                iconSample.Image = Resources.Exit_16.ToBitmap();
            }
        }

        public static void SetIcon(LoadedEnvironments currentLoadedEnvironment, LoadedSettings loadedSettings, NotifyIcon icon)
        {
            if (currentLoadedEnvironment.LoadIcon && 
                File.Exists(currentLoadedEnvironment.IconFileLocation) &&
                currentLoadedEnvironment.IconFileLocation.Contains(Constants.FileExtensions.IconExtension, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var iconFromFile = new Icon(currentLoadedEnvironment.IconFileLocation, 16, 16);
                    icon.Icon = iconFromFile;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Constants.IconMessages.ErrorLoadingIcon + ex, 
                                    Constants.IconMessages.ErrorLoadingIconCaption, 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    icon.Icon = Resources.Exit_16;
                }
            }
            else
            {
                Font font = new Font(loadedSettings.General.IconFont, loadedSettings.General.IconFontSize);
                Bitmap bmp = new Bitmap(16, 16, PixelFormat.Format32bppRgb);
			    using (Graphics g = Graphics.FromImage(bmp))
			    {
                    Rectangle rectangle = new Rectangle(0, 0, 16, 16);
			        g.FillEllipse(currentLoadedEnvironment.IconBackgroundColor.ToSolidBrush(), rectangle);
                    g.DrawString(currentLoadedEnvironment.IconLabel, font, currentLoadedEnvironment.IconTextColor.ToSolidBrush(), 0, 1);
			    }

                icon.Icon = Converter.BitmapToIcon(bmp);
            }
        }
    }
}
