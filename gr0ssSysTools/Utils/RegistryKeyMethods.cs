using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace gr0ssSysTools.Utils
{
    public class RegistryKeyMethods
    {
        public static RegistryKey GetRegistryKeyFromText(string key)
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

        public static void PopulateRootCombo(ComboBox rootCombo)
        {
            rootCombo.Items.Add(Registry.ClassesRoot.Name);
            rootCombo.Items.Add(Registry.CurrentUser.Name);
            rootCombo.Items.Add(Registry.LocalMachine.Name);
            rootCombo.Items.Add(Registry.Users.Name);
            rootCombo.Items.Add(Registry.CurrentConfig.Name);
        }

        public static void PopulateRootCombo2(ComboBox rootCombo, ComboBox rootCombo2)
        {
            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            
            foreach (var name in currentRoot.GetSubKeyNames())
            {
                rootCombo2.Items.Add(name);
            }
        }

        public static void PopulateRootCombo3(ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3)
        {
            var currentRoot = GetRegistryKeyFromText(rootCombo.SelectedText);
            var currentSubkey = currentRoot.OpenSubKey(rootCombo2.SelectedItem.ToString());

            foreach (var name in currentSubkey.GetSubKeyNames())
            {
                rootCombo3.Items.Add(name);
            }
        }

        public static string GetCurrentRoot(ComboBox rootCombo, ComboBox rootCombo2, ComboBox rootCombo3)
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
    }
}
