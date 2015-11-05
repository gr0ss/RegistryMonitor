using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using gr0ssSysTools.FileUtils;
using gr0ssSysTools.Parsers;
using gr0ssSysTools.Properties;
using gr0ssSysTools.Utils;
using Microsoft.Win32;

namespace gr0ssSysTools
{
    public partial class Settings : Form
    {
        private LoadedSettings _loadedSettings;
        private bool _loadingValues;

        public Settings()
        {
            InitializeComponent();
        }

        public Settings(LoadedSettings loadedSettings, bool env)
        {
            InitializeComponent();
            
            _loadedSettings = loadedSettings;

            tabControl.SelectedTab = env ? tabEnvironments : tabTools;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            SetupButtonImages();
                        
            if (tabControl.SelectedTab == tabEnvironments)
            {
                environmentsList.Items.Clear();
                RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                SetupButtonEnabled(false);
                RegistryKeyUtils.PopulateComboBoxesBasedOnCurrentRegistryKey(_loadedSettings.MonitoredRegistryKey, rootCombo, rootCombo2, rootCombo3, fieldTextBox);
                GlobalHotkeyUtils.PopulateGlobalHotkeyCombos(_loadedSettings.General.LoadedGlobalHotkey, hotkeyComboBox, firstModifierKeyComboBox, secondModifierKeyComboBox);
            }
            else
            {
                ClearEnvironmentFields();
                ClearToolFields();
                SetupButtonEnabled(true);
                RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);
            }
        }

#region Setup and Populate

        private void RepopulateSelectedTabsListbox(bool env)
        {
            if (env)
            {
                environmentsList.Items.Clear();

                foreach (var key in _loadedSettings.Environments)
                {
                    environmentsList.Items.Add(key.Name);
                }
            }
            else
            {
                toolsList.Items.Clear();

                foreach (var key in _loadedSettings.Tools)
                {
                    toolsList.Items.Add(key.Name);
                }
            }
        }
        
        private void SetupButtonEnabled(bool enabled)
        {
            /* Save button is always enabled. All other *
             * buttons are dependent on the current tab */
            addButton.Enabled = enabled;
            removeButton.Enabled = enabled;
            moveUpButton.Enabled = enabled;
            moveDownButton.Enabled = enabled;
        }
        
        private void SetupButtonImages()
        {
            addButton.Image = Resources.Add;
            removeButton.Image = Resources.Delete;
            saveButton.Image = Resources.Save;

            var arrowUpPicture = Resources.Move_Arrow;
            arrowUpPicture.RotateFlip(RotateFlipType.Rotate90FlipY);
            moveUpButton.Image = arrowUpPicture;

            var arrowDownPicture = Resources.Move_Arrow;
            arrowDownPicture.RotateFlip(RotateFlipType.Rotate270FlipY);
            moveDownButton.Image = arrowDownPicture;
        }

        private void PopulateHotkeyCombo()
        {
            if (tabControl.SelectedTab == tabEnvironments)
            {
                hotkeyCombo.Items.Clear();
                foreach (var c in NameTextbox.Text.Where(c => c.ToString() != " "))
                    hotkeyCombo.Items.Add(c);
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                hotkeyToolsCombo.Items.Clear();
                foreach (var c in toolsNameTextbox.Text.Where(c => c.ToString() != " "))
                    hotkeyToolsCombo.Items.Add(c);
            }
        }

        private void PopulateIconColorCombo()
        {
            iconColorCombo.Items.Clear();
            foreach (var color in ColorParser.GetAllColors())
            {
                iconColorCombo.Items.Add(new ColorDropDownItem(color, ColorParser.ConvertStringToSolidBrush(color)));
            }
        }

        private void NameTextbox_Leave(object sender, EventArgs e)
        {
            PopulateHotkeyCombo();
        }
#endregion Setup and Populate

        private void addButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabEnvironments)
            {
                var addName = new AddName(true, _loadedSettings);
                addName.Closing += NameAdded_EventHandler;
                addName.Show();
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                var addTextName = new AddName(false, _loadedSettings);
                addTextName.Closing += NameAdded_EventHandler;
                addTextName.Show();
            }
        }

        private void NameAdded_EventHandler(object sender, CancelEventArgs e)
        {
            RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);

            if (tabControl.SelectedTab == tabEnvironments)
            {
                var index = environmentsList.Items.Count - 1;
                environmentsList.SelectedIndex = index;
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                var toolIndex = toolsList.Items.Count - 1;
                toolsList.SelectedIndex = toolIndex;
            }

            // Remove Event handler
            //Events.Dispose();
        }
        
        private void removeButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabEnvironments)
            {
                if (environmentsList.SelectedIndex != -1)
                {
                    var allEnvironments = _loadedSettings.Environments;
                    var environmentToRemove = allEnvironments.FirstOrDefault(env => env.ID == Guid.Parse(guidLabel.Text));
                    if (environmentToRemove.ID != Guid.Empty)
                    {
                        allEnvironments.Remove(environmentToRemove);
                        _loadedSettings.Environments = allEnvironments;
                        ClearEnvironmentFields();
                    }
                    else
                        MessageBox.Show("Error retrieving environment", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("You need to select an environment before you can delete one...", "Duh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                if (toolsList.SelectedIndex != -1)
                {
                    var allTools = _loadedSettings.Tools;
                    var toolToRemove = allTools.FirstOrDefault(tool => tool.ID == Guid.Parse(guidToolsLabel.Text));
                    if (toolToRemove != null && toolToRemove.ID != Guid.Empty)
                    {
                        allTools.Remove(toolToRemove);
                        _loadedSettings.Tools = allTools;
                        ClearToolFields();
                    }
                    else
                        MessageBox.Show("Error retrieving tool", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("You need to select a tool before you can delete one...", "Duh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RepopulateSelectedTabsListbox(tabControl.SelectedTab == tabEnvironments);
        }

#region Clear Methods
        private void ClearEnvironmentFields()
        {
            _loadingValues = true;
            NameTextbox.Text = "";
            registryValueTextbox.Text = "";
            hotkeyCombo.Items.Clear();
            hotkeyCombo.Text = "";
            iconDisplayTextbox.Text = "";
            iconColorCombo.SelectedIndex = -1;
            guidLabel.Text = "";
            _loadingValues = false;
        }

        private void ClearToolFields()
        {
            _loadingValues = true;
            toolsNameTextbox.Text = "";
            DirectoryPathTextbox.Text = "";
            hotkeyToolsCombo.Items.Clear();
            hotkeyToolsCombo.Text = "";
            guidToolsLabel.Text = "";
            _loadingValues = false;
        }
#endregion Clear Methods

#region Save button
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                SaveNewRegistryKey();
                SaveNewGlobalHotkey();
            }
            else
            {
                if (tabControl.SelectedTab == tabEnvironments)
                {
                    SaveCurrentEnvironment();
                }
                else if (tabControl.SelectedTab == tabTools)
                {
                    SaveCurrentTool();
                }
            }
        }

        private void SaveNewGlobalHotkey()
        {
            if (firstModifierKeyComboBox.SelectedText == System.Windows.Input.ModifierKeys.None.ToString())
            {
                MessageBox.Show("You must first select a valid modifier key.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } 
            else if (CurrentHotkeyEqualsSavedHotkey()) // If Global Hotkey is the same as LoadedGlobalHotkey then do nothing.
            {
                return;
            }
            else
            {
                _loadedSettings.General.LoadedGlobalHotkey.Hotkey =
                    GlobalHotkeyParser.ConvertStringToKey(hotkeyComboBox.SelectedText);
                _loadedSettings.General.LoadedGlobalHotkey.FirstModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(firstModifierKeyComboBox.SelectedText);
                _loadedSettings.General.LoadedGlobalHotkey.SecondModifierKey =
                    GlobalHotkeyParser.ConvertStringToModifierKeys(secondModifierKeyComboBox.SelectedText);
            }
        }

        private bool CurrentHotkeyEqualsSavedHotkey()
        {
            return _loadedSettings.General.LoadedGlobalHotkey.Hotkey.ToString() ==
                    hotkeyComboBox.SelectedText &&
                   _loadedSettings.General.LoadedGlobalHotkey.FirstModifierKey.ToString() ==
                    firstModifierKeyComboBox.SelectedText &&
                   _loadedSettings.General.LoadedGlobalHotkey.SecondModifierKey.ToString() ==
                    secondModifierKeyComboBox.SelectedText;
        }

        private void SaveCurrentEnvironment()
        {
            var currentEnvironment = _loadedSettings.Environments.First(env => env.ID == Guid.Parse(guidLabel.Text));

            if (currentEnvironment.Name != NameTextbox.Text)
                currentEnvironment.Name = NameTextbox.Text;
            if (currentEnvironment.SubkeyValue != registryValueTextbox.Text)
                currentEnvironment.SubkeyValue = registryValueTextbox.Text;
            if (currentEnvironment.HotKey != hotkeyCombo.SelectedItem.ToString())
                currentEnvironment.HotKey = hotkeyCombo.SelectedItem.ToString();
            if (currentEnvironment.IconLabel != iconDisplayTextbox.Text)
                currentEnvironment.IconLabel = iconDisplayTextbox.Text;
            if (currentEnvironment.IconColor != iconColorCombo.SelectedItem.ToString())
                currentEnvironment.IconColor = iconColorCombo.SelectedItem.ToString();

            RepopulateSelectedTabsListbox(true);
            SetCurrentOrderOfEnvironmentsAndSave();
        }

        private void SaveCurrentTool()
        {
            var currentTool = _loadedSettings.Tools.First(tool => tool.ID == Guid.Parse(guidToolsLabel.Text));

            if (currentTool.Name != toolsNameTextbox.Text)
                currentTool.Name = toolsNameTextbox.Text;
            if (currentTool.FileLocation != DirectoryPathTextbox.Text)
                currentTool.FileLocation = DirectoryPathTextbox.Text;
            if (currentTool.HotKey != hotkeyToolsCombo.SelectedItem.ToString())
                currentTool.HotKey = hotkeyToolsCombo.SelectedItem.ToString();

            RepopulateSelectedTabsListbox(false);
            SetCurrentOrderOfToolsAndSave();
        }

        private void SetCurrentOrderOfEnvironmentsAndSave()
        {
            var environmentsOrdered = (from object item 
                                       in environmentsList.Items
                                       select _loadedSettings.Environments.FirstOrDefault(env => env.Name == item.ToString()))
                                       .ToList();
            _loadedSettings.Environments = environmentsOrdered;
        }

        private void SetCurrentOrderOfToolsAndSave()
        {
            var toolsOrdered = (from object item 
                                in toolsList.Items
                                select _loadedSettings.Tools.FirstOrDefault(tool => tool.Name == item.ToString()))
                                .ToList();
            _loadedSettings.Tools = toolsOrdered;
        }
#endregion Save button

#region Move buttons
        private void moveUpButton_Click(object sender, EventArgs e)
        {
            TurnOffListEventHandlers();
            MoveItem(-1);
            TurnOnListEventHandlers();
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            TurnOffListEventHandlers();
            MoveItem(1);
            TurnOnListEventHandlers();
        }

        private void environmentsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (environmentsList.SelectedIndex != -1)
            {
                _loadingValues = true;
                var curItem = environmentsList.SelectedItem.ToString();
                var itemToLoad = _loadedSettings.Environments.FirstOrDefault(env => env.Name == curItem);

                guidLabel.Text = itemToLoad.ID.ToString();
                NameTextbox.Text = curItem;
                registryValueTextbox.Text = itemToLoad.SubkeyValue;

                PopulateHotkeyCombo();
                hotkeyCombo.SelectedIndex = MiscUtils.GetIndexOfHotkey(itemToLoad.Name, itemToLoad.HotKey);

                iconDisplayTextbox.Text = itemToLoad.IconLabel;

                PopulateIconColorCombo();

                var colorIndex = Array.FindIndex(ColorParser.GetAllColors(), row => row.Contains(itemToLoad.IconColor));
                iconColorCombo.SelectedItem = iconColorCombo.Items[colorIndex];
            }
            _loadingValues = false;
        }

        private void toolsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolsList.SelectedIndex != -1)
            {
                _loadingValues = true;
                var curItem = toolsList.SelectedItem.ToString();
                var itemToLoad = _loadedSettings.Tools.First(tool => tool.Name == curItem);

                guidToolsLabel.Text = itemToLoad.ID.ToString();
                toolsNameTextbox.Text = curItem;
                DirectoryPathTextbox.Text = itemToLoad.FileLocation;

                PopulateHotkeyCombo();
                hotkeyToolsCombo.SelectedIndex = MiscUtils.GetIndexOfHotkey(itemToLoad.Name, itemToLoad.HotKey);
            }
            _loadingValues = false;
        }

        private void TurnOffListEventHandlers()
        {
            environmentsList.SelectedIndexChanged -= environmentsList_SelectedIndexChanged;
            toolsList.SelectedIndexChanged -= toolsList_SelectedIndexChanged;
        }

        private void TurnOnListEventHandlers()
        {
            environmentsList.SelectedIndexChanged += environmentsList_SelectedIndexChanged;
            toolsList.SelectedIndexChanged += toolsList_SelectedIndexChanged;
        }

        private void MoveItem(int direction)
         {
            if (tabControl.SelectedTab == tabEnvironments)
            {
                // Checking selected item
                if (environmentsList.SelectedItem == null || environmentsList.SelectedIndex < 0)
                    return; // No selected item - nothing to do

                // Calculate new index using move direction
                int newIndex = environmentsList.SelectedIndex + direction;

                // Checking bounds of the range
                if (newIndex < 0 || newIndex >= environmentsList.Items.Count)
                    return; // Index out of range - nothing to do

                object selected = environmentsList.SelectedItem;

                // Removing removable element
                environmentsList.Items.Remove(selected);
                // Insert it in new position
                environmentsList.Items.Insert(newIndex, selected);
                // Restore selection
                environmentsList.SetSelected(newIndex, true);
                // Save the new order
                SetCurrentOrderOfEnvironmentsAndSave();
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                // Checking selected item
                if (toolsList.SelectedItem == null || toolsList.SelectedIndex < 0)
                    return; // No selected item - nothing to do

                // Calculate new index using move direction
                int newIndex = toolsList.SelectedIndex + direction;

                // Checking bounds of the range
                if (newIndex < 0 || newIndex >= toolsList.Items.Count)
                    return; // Index out of range - nothing to do

                object selected = toolsList.SelectedItem;

                // Removing removable element
                toolsList.Items.Remove(selected);
                // Insert it in new position
                toolsList.Items.Insert(newIndex, selected);
                // Restore selection
                toolsList.SetSelected(newIndex, true);
                // Save the new order
                SetCurrentOrderOfToolsAndSave();
            }
        }
#endregion Move buttons

#region Registry Key Methods
        private void checkButton_Click(object sender, EventArgs e)
        {
            var rootValue = RegistryKeyUtils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3);

            MessageBox.Show($"The current registry key selected is:\n{rootValue}\\{fieldTextBox.Text}\n\nIt has a value of:\n{GetCurrentKeyValue()}", @"Current Value of Key",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveNewRegistryKey()
        {
            if (GetCurrentKeyValue() == string.Empty) // New Key is invalid.
            {
                MessageBox.Show("You must first select a valid key.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (CurrentKeyEqualsSavedKey()) // New Key is Old Key.
            {
                return;
            }
            else // Save New Key.
            {
                var confirmMessage = MessageBox.Show("Are you sure this is what you want to override the currently monitored registry key?", 
                    "Continue With Save", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

                if (confirmMessage != DialogResult.Yes) return;

                var newRegistryKey = new Files.MonitoredRegistryKey
                {
                    Root = RegistryKeyUtils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3),
                    Subkey = fieldTextBox.Text
                };
                _loadedSettings.MonitoredRegistryKey = newRegistryKey;
            }
        }

        private void RootCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo2.Items.Clear();
            rootCombo2.Text = "";
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";
            RegistryKeyUtils.PopulateRootCombo2(rootCombo, rootCombo2);
        }

        private void RootCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";

            RegistryKeyUtils.PopulateRootCombo3(rootCombo, rootCombo2, rootCombo3);
        }

        private bool CurrentKeyEqualsSavedKey()
        {
            var currentRoot = RegistryKeyUtils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3);

            return currentRoot == _loadedSettings.MonitoredRegistryKey.Root &&
                   fieldTextBox.Text == _loadedSettings.MonitoredRegistryKey.Subkey;
        }

        private string GetCurrentKeyValue()
        {
            return (string) Registry.GetValue(RegistryKeyUtils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3).ToString(), fieldTextBox.Text, "");
        }

        #endregion Registry Key Methods
    }
}
