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
                if (_environments == null || _environments.Count <= 0)
                {
                    _environments = EnvironmentUtils.ReadEnvironmentsSettings();
                }
                return _environments;
            }
            set
            {
                EnvironmentUtils.WriteEnvironmentSettings(value);
                _environments = value;
            }
        }

        private List<Tools> _tools;

        public List<Tools> Tools
        {
            get
            {
                if (_tools == null || _tools.Count <= 0)
                {
                    _tools = ToolsUtils.ReadToolsSettings();
                }
                return _tools;
            }
            set
            {
                ToolsUtils.WriteToolsSettings(value);
                _tools = value;
            }
        }

        private RegistryKey _registryKey;

        public RegistryKey RegistryKey
        {
            get
            {
                if (string.IsNullOrEmpty(_registryKey?.Root))
                {
                    _registryKey = RegistryKeyUtils.ReadRegistryKeySettings();
                }
                return _registryKey;
            }
            set
            {
                RegistryKeyUtils.WriteRegistryKeySettings(value);
                _registryKey = value;
            }
        }

        private General _general;

        public General General
        {
            get
            {
                if (string.IsNullOrEmpty(_general?.IconFont))
                {
                    _general = GeneralUtils.ReadGeneralSettings();
                }
                return _general;
            }
            set
            {
                GeneralUtils.WriteGeneralSettings(value);
                _general = value;
            }
        }
    }
}
