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
                    _environments = EnvironmentUtils.ReadEnvironmentsSettingsJson();
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
                if (_tools == null || _tools.Count <= 0)
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
                if (_registryKey == null)
                {
                    _registryKey = RegistryKeyUtils.ReadRegistryKeySettingsJson();
                }
                return _registryKey;
            }
            set { _registryKey = value; }
        }

        private General _general;

        public General General
        {
            get
            {
                if (_general == null)
                {
                    _general = GeneralUtils.ReadGeneralSettingsJson();
                }
                return _general;
            }
            set { _general = value; }
        }
    }
}
