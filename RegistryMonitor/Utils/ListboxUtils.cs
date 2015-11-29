using System;
using System.Linq;
using System.Windows.Forms;

namespace RegistryMonitor.Utils
{
    public static class ListboxUtils
    {
        public static void RepopulateListBox(bool env, ListBox currentListBox, LoadedSettings loadedSettings, Guid currentGuid)
        {
            currentListBox.Items.Clear();

            if (env)
            {
                foreach (var key in loadedSettings.Environments)
                {
                    currentListBox.Items.Add(key.Name);
                }
            }
            else
            {
                foreach (var key in loadedSettings.Tools)
                {
                    currentListBox.Items.Add(key.Name);
                }
            }

            
            if (currentGuid != Guid.Empty) return;

            var currentName = env
                            ? loadedSettings.Environments.FirstOrDefault(environment => environment.ID == currentGuid)?.Name
                            : loadedSettings.Tools.FirstOrDefault(tool => tool.ID == currentGuid)?.Name;
            
            if (currentName != null)
                currentListBox.SelectedIndex = currentListBox.Items.IndexOf(currentName);
        }

        public static void RemoveListBoxItem(bool env, ListBox currentListBox, LoadedSettings loadedSettings, string currentGuid)
        {
            if (currentListBox.SelectedIndex != -1)
            {
                if (env)
                {
                    var allEnvironments = loadedSettings.Environments;
                    var environmentToRemove = allEnvironments.FirstOrDefault(environment => environment.ID == Guid.Parse(currentGuid));
                    if (environmentToRemove?.ID != Guid.Empty)
                    {
                        allEnvironments.Remove(environmentToRemove);
                        loadedSettings.Environments = allEnvironments;
                    }
                    else
                    {
                        MessageBox.Show(Constants.EnvironmentMessages.ErrorRetrievingEnvironment,
                                        Constants.EnvironmentMessages.ErrorRetrievingEnvironmentCaption, 
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var allTools = loadedSettings.Tools;
                    var toolToRemove = allTools.FirstOrDefault(tool => tool.ID == Guid.Parse(currentGuid));
                    if (toolToRemove != null && toolToRemove.ID != Guid.Empty)
                    {
                        allTools.Remove(toolToRemove);
                        loadedSettings.Tools = allTools;
                    }
                    else
                    {
                        MessageBox.Show(Constants.ToolMessages.ErrorRetrievingTool,
                            Constants.ToolMessages.ErrorRetrievingToolCaption,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (env)
                {
                    MessageBox.Show(Constants.EnvironmentMessages.SelectEnvironmentToDelete, 
                                Constants.EnvironmentMessages.SelectEnvironmentToDeleteCaption, 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(Constants.ToolMessages.SelectToolToDelete, 
                                    Constants.ToolMessages.SelectToolToDeleteCaption, 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        public static void MoveItem(int direction, bool env, ListBox currentListBox, LoadedSettings loadedSettings)
        {
            // Checking selected item
            if (currentListBox.SelectedItem == null || currentListBox.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = currentListBox.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= currentListBox.Items.Count)
                return; // Index out of range - nothing to do

            object selected = currentListBox.SelectedItem;

            // Removing removable element
            currentListBox.Items.Remove(selected);
            // Insert it in new position
            currentListBox.Items.Insert(newIndex, selected);
            // Restore selection
            currentListBox.SetSelected(newIndex, true);
            // Save changes
            SetCurrentOrderFromListBoxAndSave(env, currentListBox, loadedSettings);
        }

        public static void SetCurrentOrderFromListBoxAndSave(bool env, ListBox currentListBox, LoadedSettings loadedSettings)
        {
            if (env)
            {
                var environmentsOrdered = (from object item 
                                           in currentListBox.Items
                                           select loadedSettings.Environments.FirstOrDefault(environment => environment.Name == item.ToString()))
                                           .ToList();
                loadedSettings.Environments = environmentsOrdered;
            }
            else
            {
                var toolsOrdered = (from object item 
                                    in currentListBox.Items
                                    select loadedSettings.Tools.FirstOrDefault(tool => tool.Name == item.ToString()))
                                    .ToList();
                loadedSettings.Tools = toolsOrdered;
            }
            
        }
    }
}
