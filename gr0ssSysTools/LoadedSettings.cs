using System.Collections.Generic;
using gr0ssSysTools.Files;
using gr0ssSysTools.FileUtils;

namespace gr0ssSysTools
{
    public class LoadedSettings
    {
        private List<LoadedEnvironments> _environments;

        public List<LoadedEnvironments> Environments
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

        private List<LoadedTools> _tools;

        public List<LoadedTools> Tools
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

        private MonitoredRegistryKey _monitoredRegistryKey;

        public MonitoredRegistryKey MonitoredRegistryKey
        {
            get
            {
                if (string.IsNullOrEmpty(_monitoredRegistryKey?.Root))
                {
                    _monitoredRegistryKey = RegistryKeyUtils.ReadRegistryKeySettings();
                }
                return _monitoredRegistryKey;
            }
            set
            {
                RegistryKeyUtils.WriteRegistryKeySettings(value);
                _monitoredRegistryKey = value;
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
