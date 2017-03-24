using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows;

namespace ZtxFrameWork.UI.Helpers
{
    public static class ConfigurationHelper
    {
        public const string SettingsNamespace = "DXThemeManager";
        const string ApplicationThemeNameSettingsName = "ApplicationThemeName";
        const string UserSettingsGroupName = "userSettings";
        static Configuration LoadConfiguration(string appConfigPath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            System.Configuration.Configuration machineConfiguration = System.Configuration.ConfigurationManager.OpenMachineConfiguration();
            fileMap.MachineConfigFilename = machineConfiguration.FilePath;
            fileMap.ExeConfigFilename = appConfigPath;
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
        static UserSettingsGroup GetUserSettingsGroup(Configuration configuration)
        {
            UserSettingsGroup userSettingsGroup = configuration.GetSectionGroup(UserSettingsGroupName) as UserSettingsGroup;
            if (userSettingsGroup == null)
            {
                userSettingsGroup = new UserSettingsGroup();
                configuration.SectionGroups.Add(UserSettingsGroupName, userSettingsGroup);
            }
            return userSettingsGroup;
        }
        static ClientSettingsSection GetClientSection(UserSettingsGroup userSettingsGroup)
        {
            ClientSettingsSection clientSection = userSettingsGroup.Sections[SettingsNamespace] as ClientSettingsSection;
            if (clientSection == null)
            {
                clientSection = new ClientSettingsSection();
                userSettingsGroup.Sections.Add(SettingsNamespace, clientSection);
                clientSection.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
                clientSection.SectionInformation.RequirePermission = false;
            }
            return clientSection;
        }
        static void SaveApplicationThemeName(string themeName, Configuration configuration, ClientSettingsSection clientSection)
        {
            var settingElement = new SettingElement(ApplicationThemeNameSettingsName, SettingsSerializeAs.String);
            var valueNode = new System.Xml.XmlDocument().CreateElement("value");
            valueNode.InnerText = themeName;
            settingElement.Value.ValueXml = valueNode;
            clientSection.Settings.Add(settingElement);
            configuration.Save();
        }
        public static string GetThemeNameFromConfig()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return string.Empty;
            string appConfigPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            return GetApplicationThemeNameFromConfig(appConfigPath);
        }
        public static void SaveThemeNameToConfig(string themeName)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            string appConfigPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            SaveApplicationThemeNameToConfig(appConfigPath, themeName);
        }
        public static string GetApplicationThemeNameFromConfig(string appConfigPath)
        {
            Configuration configuration = ConfigurationHelper.LoadConfiguration(appConfigPath);
            UserSettingsGroup userSettingsGroup = ConfigurationHelper.GetUserSettingsGroup(configuration);
            ClientSettingsSection clientSection = ConfigurationHelper.GetClientSection(userSettingsGroup);
            SettingElement elem = clientSection.Settings.Get(ApplicationThemeNameSettingsName);
            if (elem != null && elem.Value != null && elem.Value.ValueXml != null)
            {
                return elem.Value.ValueXml.InnerText;
            }
            return null;
        }
        public static void RemoveThemeNameFromConfig(string appConfigPath)
        {
            Configuration conf = LoadConfiguration(appConfigPath);
            UserSettingsGroup userSettingsGroup = conf.GetSectionGroup(UserSettingsGroupName) as UserSettingsGroup;
            if (userSettingsGroup != null)
            {
                ClientSettingsSection clientSection = userSettingsGroup.Sections[SettingsNamespace] as ClientSettingsSection;
                if (clientSection != null)
                {
                    userSettingsGroup.Sections.Remove(ApplicationThemeNameSettingsName);
                }
                conf.SectionGroups.Remove(UserSettingsGroupName);
                conf.Save();
            }
        }
        public static void SaveApplicationThemeNameToConfig(string appConfigPath, string themeName)
        {
            Configuration configuration = ConfigurationHelper.LoadConfiguration(appConfigPath);
            UserSettingsGroup userSettingsGroup = ConfigurationHelper.GetUserSettingsGroup(configuration);
            ClientSettingsSection clientSection = ConfigurationHelper.GetClientSection(userSettingsGroup);
            SaveApplicationThemeName(themeName, configuration, clientSection);
        }
    }
}
