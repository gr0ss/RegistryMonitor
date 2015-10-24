using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComboxExtended;
using Microsoft.Win32;

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

        public int GetIndexOfHotkey(string name, string hotkey)
        {
            var indexOfHotkey = name.IndexOf(hotkey, StringComparison.Ordinal);
            var numberOfSpacesBeforeHotkey = name.Substring(0, indexOfHotkey).Count(char.IsSeparator);
            return indexOfHotkey - numberOfSpacesBeforeHotkey;
        }

        #region Registry Key Utils
        public RegistryKey GetRegistryKeyFromText(string key)
        {
            if (key == Registry.ClassesRoot.Name)
                return Registry.ClassesRoot;
            if (key == Registry.CurrentUser.Name)
                return Registry.CurrentUser;
            if (key == Registry.LocalMachine.Name)
                return Registry.LocalMachine;
            if (key == Registry.Users.Name)
                return Registry.Users;
            if (key == Registry.CurrentConfig.Name)
                return Registry.CurrentConfig;
            return Registry.CurrentUser;
        }

        public void PopulateRootCombo(ComboBox rootCombo)
        {
            rootCombo.Items.Add(Registry.ClassesRoot.Name);
            rootCombo.Items.Add(Registry.CurrentUser.Name);
            rootCombo.Items.Add(Registry.LocalMachine.Name);
            rootCombo.Items.Add(Registry.Users.Name);
            rootCombo.Items.Add(Registry.CurrentConfig.Name);
        }

        public void PopulateRootCombo2(ComboBox rootCombo, ComboBox rootCombo2)
        {
            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            
            foreach (var name in currentRoot.GetSubKeyNames())
            {
                rootCombo2.Items.Add(name);
            }
        }

        public void PopulateRootCombo3(ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3)
        {
            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            var currentSubkey = currentRoot.OpenSubKey(rootCombo2.SelectedItem.ToString());

            foreach (var name in currentSubkey.GetSubKeyNames())
            {
                rootCombo3.Items.Add(name);
            }
        }

        public string GetCurrentRoot(ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3)
        {
            var userRoot = new StringBuilder();
            if (rootCombo.SelectedIndex != -1)
                userRoot.Append(rootCombo.SelectedItem);
            if (rootCombo2.SelectedIndex != -1)
                userRoot.Append("\\" + rootCombo2.SelectedItem);
            if (rootCombo3.SelectedIndex != -1)
                userRoot.Append("\\" + rootCombo3.SelectedItem);
            return userRoot.ToString();
        }
        #endregion Registry Key Utils
    }
}
