using System.Collections.Generic;
using gr0ssSysTools.Files;
using gr0ssSysTools.FileUtils;

namespace gr0ssSysTools
{
    public class Settings
    {
        private List<Environments> _environments;

        public List<Environments> Environments
        {
            get
            {
                if (_environments.Count <= 0)
                {
                    _environments = EnvironmentUtils.ReadJsonEnvironmentsSettings();
                }
                return _environments;
            }
            set { _environments = value; }
        }

        private List<Tools> _tools;

        public List<Tools> Tools
        {
            get
            {
                if (_tools.Count <= 0)
                {
                    _tools = ToolsUtils.ReadToolsSettingsJson();
                }
                return _tools;
            }
            set { _tools = value; }
        }

        private RegistryKey _registryKey;

        public RegistryKey RegistryKey
        {
            get
            {
                if (string.IsNullOrEmpty(_registryKey.Root))
                {
                    _registryKey = RegistryKeyUtils.ReadJsonRegistryKeySettings();
                }
                return _registryKey;
            }
            set { _registryKey = value; }
        }

        
    }
}
