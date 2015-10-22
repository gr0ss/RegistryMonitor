using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using gr0ssSysTools.FileUtils;
using gr0ssSysTools.Properties;

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

            _utils = new MiscUtils();
        }

        public Edit(bool env)
        {
            InitializeComponent();
            
            _dir = new DirectoryUtils();

            tabControl.SelectedTab = env ? tabEnvironments : tabTools;
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            SetupButtonImages();

            _dir.CreateTextIfItDoesntExist(_environmentsText);
            _dir.CreateTextIfItDoesntExist(_toolsText);

            RepopulateListFromFile(tabControl.SelectedTab == tabEnvironments, false);
        }

        private void environmentsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curItem = environmentsList.SelectedItem.ToString();
            var itemToLoad = _environments.FirstOrDefault(env => env.Name == curItem);

            guidLabel.Text = itemToLoad.ID.ToString();
            NameTextbox.Text = curItem;
            registryValueTextbox.Text = itemToLoad.ValueKey;

            PopulateHotkeyCombo();
            hotkeyCombo.SelectedIndex = itemToLoad.Name.IndexOf(itemToLoad.HotKey, StringComparison.Ordinal);

            iconDisplayTextbox.Text = itemToLoad.IconLabel;

            PopulateIconColorCombo();
            var colorIndex = _utils.GetColorIndex(itemToLoad.IconColor);
            iconColorCombo.SelectedItem = iconColorCombo.Items[colorIndex];
        }
        
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
            hotkeyCombo.Items.Clear();
            foreach (var c in NameTextbox.Text)
            {
                if (c.ToString() != " ")
                    hotkeyCombo.Items.Add(c);
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

        private void addButton_Click(object sender, EventArgs e)
        {
            //if (tabControl.SelectedTab == tabEnvironments)
            //    if (NameTextbox.Text != string.Empty && registryValueTextbox.Text != string.Empty)
            //        _environments.Add(NameTextbox.Text, registryValueTextbox.Text);
            //    else
            //    {
            //        MessageBox.Show("Please fill out the name and registry value fields before adding", "Error",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //else if (tabControl.SelectedTab == tabTools)
            //    if (toolsNameTextbox.Text != string.Empty && DirectoryPathTextbox.Text != string.Empty)
            //        _tools.Add(toolsNameTextbox.Text, DirectoryPathTextbox.Text);
            //    else
            //    {
            //        MessageBox.Show("Please fill out the name and directory path fields before adding", "Error",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //RepopulateListFromFile(tabControl.SelectedTab == tabEnvironments, true);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabEnvironments)
            {
                if (environmentsList.SelectedIndex != -1)
                {
                    var environmentToRemove = _environments.FirstOrDefault(env => env.ID == Guid.Parse(guidLabel.Text));
                    if (environmentToRemove.ID != Guid.Empty)
                        _environments.Remove(environmentToRemove);
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
                        _tools.Remove(toolToRemove);
                    else
                        MessageBox.Show("Error retrieving tool", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("You need to select a tool before you can delete one...", "Duh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RepopulateListFromFile(tabControl.SelectedTab == tabEnvironments, true);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SetDictionaryIndexes();

            if (tabControl.SelectedTab == tabEnvironments)
                _dir.SaveListToFile(_environments, _environmentsText);
            else if (tabControl.SelectedTab == tabTools)
                _dir.SaveListToFile(_tools, _toolsText);
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            environmentsList.SelectedIndexChanged -= environmentsList_SelectedIndexChanged;
            MoveItem(-1);
            environmentsList.SelectedIndexChanged += environmentsList_SelectedIndexChanged;
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            environmentsList.SelectedIndexChanged -= environmentsList_SelectedIndexChanged;
            MoveItem(1);
            environmentsList.SelectedIndexChanged += environmentsList_SelectedIndexChanged;
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

        private void SetDictionaryIndexes()
        {
            var dictionaryToCopy = new List<FileStruct>();
            var listIndexes = GetListIndex();

            if (tabControl.SelectedTab == tabEnvironments)
            {
                if (_environments == new List<FileStruct>() || listIndexes == new Dictionary<int, string>())
                    return;

                dictionaryToCopy.AddRange(listIndexes.OrderBy(list => list.Key)
                                                     .Select(index => _environments.First(env => env.Name == listIndexes.Values.FirstOrDefault())));
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
    }
}
