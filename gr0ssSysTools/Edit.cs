using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using gr0ssSysTools.FileUtils;
using gr0ssSysTools.Properties;
using Microsoft.Win32;

namespace gr0ssSysTools
{
    public partial class Edit : Form
    {
        private string _environmentsText = "\\environments.txt";
        private string _toolsText = "\\tools.txt";
        private DirectoryUtils _dir;
        private List<FileStruct> _environments;
        private List<FileStruct> _tools; 
        private MiscUtils _utils;

        public Edit()
        {
            InitializeComponent();
        }

        public Edit(bool env)
        {
            InitializeComponent();
            
            _dir = new DirectoryUtils();
            _utils = new MiscUtils();

            tabControl.SelectedTab = env ? tabEnvironments : tabTools;
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            SetupButtonImages();

            _dir.CreateTextIfItDoesntExist(_environmentsText);
            _dir.CreateTextIfItDoesntExist(_toolsText);

            RepopulateListFromFile(tabControl.SelectedTab == tabEnvironments, false);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                SetupButtonEnabled(false);
                _utils.PopulateRootCombo(rootCombo);
            }
            else
            {
                SetupButtonEnabled(true);
                RepopulateListFromFile(tabControl.SelectedTab == tabEnvironments, false);
            }
        }

        #region Setup and Populate
        private void RepopulateListFromFile(bool env, bool useDictionary)
        {
            if (env)
            {
                environmentsList.Items.Clear();

                if (!useDictionary)
                    _environments = _dir.ReadFileAndPopulateList(_environmentsText);

                foreach (var key in _environments)
                {
                    environmentsList.Items.Add(key.Name);
                }
            }
            else
            {
                toolsList.Items.Clear();

                if (!useDictionary)
                    _tools = _dir.ReadFileAndPopulateList(_toolsText);

                foreach (var key in _tools)
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
            iconColorCombo.Items.ItemsBase.Clear();
            iconColorCombo.Items.Clear();

            _utils = new MiscUtils();

            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Dark Gray", Brushes.DarkGray));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Blue", Brushes.Blue));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Brown", Brushes.Brown));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Coral", Brushes.Coral));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Red", Brushes.Red));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Aqua", Brushes.Aqua));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Violet", Brushes.Violet));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Green", Brushes.Green));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Yellow", Brushes.Yellow));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Magenta", Brushes.Magenta));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Wheat", Brushes.Wheat));
            iconColorCombo.Items.Add(_utils.CreateComboBoxItem("Orange", Brushes.Orange));
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
                var addName = new AddName(true, _environments);
                addName.Closing += NameAdded_EventHandler;
                addName.Show();
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                var addTextName = new AddName(false, _tools);
                addTextName.Closing += NameAdded_EventHandler;
                addTextName.Show();
            }
        }

        private void NameAdded_EventHandler(object sender, CancelEventArgs e)
        {
            RepopulateListFromFile(tabControl.SelectedTab == tabEnvironments, false);

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
                    var environmentToRemove = _environments.FirstOrDefault(env => env.ID == Guid.Parse(guidLabel.Text));
                    if (environmentToRemove.ID != Guid.Empty)
                    {
                        _environments.Remove(environmentToRemove);
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
                    var toolToRemove = _tools.First(tool => tool.ID == Guid.Parse(guidToolsLabel.Text));
                    if (toolToRemove.ID != Guid.Empty)
                    {
                        _tools.Remove(toolToRemove);
                        ClearToolFields();
                    }
                    else
                        MessageBox.Show("Error retrieving tool", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("You need to select a tool before you can delete one...", "Duh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RepopulateListFromFile(tabControl.SelectedTab == tabEnvironments, true);
        }

        #region Clear Methods
        private void ClearEnvironmentFields()
        {
            NameTextbox.Text = "";
            registryValueTextbox.Text = "";
            hotkeyCombo.Items.Clear();
            hotkeyCombo.Text = "";
            iconDisplayTextbox.Text = "";
            iconColorCombo.SelectedIndex = -1;
            guidLabel.Text = "";
        }

        private void ClearToolFields()
        {
            toolsNameTextbox.Text = "";
            DirectoryPathTextbox.Text = "";
            hotkeyToolsCombo.Items.Clear();
            hotkeyToolsCombo.Text = "";
            guidToolsLabel.Text = "";
        }
        #endregion Clear Methods

        #region Save button
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
                SaveNewRegistryKey();
            else
            {
                SetDictionaryIndexes();

                if (tabControl.SelectedTab == tabEnvironments)
                    _dir.SaveListToFile(_environments, _environmentsText);
                else if (tabControl.SelectedTab == tabTools)
                    _dir.SaveListToFile(_tools, _toolsText);
            }
        }

        private void SetDictionaryIndexes()
        {
            var dictionaryToCopy = new List<FileStruct>();
            var listIndexes = GetListIndex();

            if (tabControl.SelectedTab == tabEnvironments)
            {
                if (_environments == new List<FileStruct>() || listIndexes == new Dictionary<int, string>())
                    return;

                dictionaryToCopy.AddRange(listIndexes.OrderBy(list => list.Key)
                                                     .Select(index => _environments.First(env => env.Name == index.Value)));
                
                _environments = dictionaryToCopy;
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                if (_tools == new List<FileStruct>() || listIndexes == new Dictionary<int, string>())
                    return;

                dictionaryToCopy.AddRange(listIndexes.OrderBy(list => list.Key)
                                                     .Select(index => _tools.First(env => env.Name == listIndexes.Values.FirstOrDefault())));
                
                _tools = dictionaryToCopy;
            }
        }

        private Dictionary<int, string> GetListIndex()
        {
            var dictionaryToReturn = new Dictionary<int, string>();
            if (tabControl.SelectedTab == tabEnvironments)
            {
                foreach (var item in environmentsList.Items)
                {
                    var text = environmentsList.GetItemText(item);
                    var index = environmentsList.Items.IndexOf(item);
                    dictionaryToReturn.Add(index, text);
                }
            }
            else if (tabControl.SelectedTab == tabTools)
            {
                foreach (var item in toolsList.Items)
                {
                    var text = toolsList.GetItemText(item);
                    var index = toolsList.Items.IndexOf(item);
                    dictionaryToReturn.Add(index, text);
                }
            }
            return dictionaryToReturn;
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
                var curItem = environmentsList.SelectedItem.ToString();
                var itemToLoad = _environments.FirstOrDefault(env => env.Name == curItem);

                guidLabel.Text = itemToLoad.ID.ToString();
                NameTextbox.Text = curItem;
                registryValueTextbox.Text = itemToLoad.ValueKey;

                PopulateHotkeyCombo();
                if (_utils == null)
                    _utils = new MiscUtils();
                hotkeyCombo.SelectedIndex = _utils.GetIndexOfHotkey(itemToLoad.Name, itemToLoad.HotKey);

                iconDisplayTextbox.Text = itemToLoad.IconLabel;

                PopulateIconColorCombo();
                var colorIndex = _utils.GetColorIndex(itemToLoad.IconColor);
                iconColorCombo.SelectedItem = iconColorCombo.Items[colorIndex];
            }
        }

        private void toolsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolsList.SelectedIndex != -1)
            {
                var curItem = toolsList.SelectedItem.ToString();
                var itemToLoad = _tools.FirstOrDefault(tool => tool.Name == curItem);

                guidToolsLabel.Text = itemToLoad.ID.ToString();
                toolsNameTextbox.Text = curItem;
                DirectoryPathTextbox.Text = itemToLoad.ValueKey;

                PopulateHotkeyCombo();
                if (_utils == null)
                    _utils = new MiscUtils();
                hotkeyToolsCombo.SelectedIndex = _utils.GetIndexOfHotkey(itemToLoad.Name, itemToLoad.HotKey);
            }
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
            }
        }
        #endregion Move buttons

        #region Registry Key Methods
        private void checkButton_Click(object sender, EventArgs e)
        {
            var rootValue = _utils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3);

            MessageBox.Show($"The current registry key selected is:\n{rootValue}\\{fieldTextBox.Text}\n\nIt has a value of:\n{GetCurrentKeyValue()}", @"Current Value of Key",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveNewRegistryKey()
        {
            if (GetCurrentKeyValue() == string.Empty)
                MessageBox.Show("You must first select a valid key.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            else
            {
                var confirmMessage = MessageBox.Show("If we save the new registry key, you must restart the program for the new key to take effect.\nAre you sure this is what you want to do?", 
                    "Continue With Save", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);

                if (confirmMessage == DialogResult.Yes)
                {
                    var newGeneralStruct = new GeneralStruct
                    {
                        RegistryRoot = _utils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3),
                        RegistryField = fieldTextBox.Text
                    };
                    var dir = new DirectoryUtils();
                    dir.SaveGeneralStructToFile(newGeneralStruct);
                }
            }
        }

        private void RootCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo2.Items.Clear();
            rootCombo2.Text = "";
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";
            _utils.PopulateRootCombo2(rootCombo, rootCombo2);
        }

        private void RootCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rootCombo3.Items.Clear();
            rootCombo3.Text = "";

            _utils.PopulateRootCombo3(rootCombo, rootCombo2, rootCombo3);
        }

        private string GetCurrentKeyValue()
        {
            return (string) Registry.GetValue(_utils.GetCurrentRoot(rootCombo, rootCombo2, rootCombo3).ToString(), fieldTextBox.Text, "");
        }
        #endregion Registry Key Methods

        
    }
}
