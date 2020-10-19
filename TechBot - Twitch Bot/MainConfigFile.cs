using Salaros.Configuration;

namespace TechBot
{
    static class MainConfigFile
    {
        private static ConfigParser ConfigFile = new ConfigParser(@"config.cfg");

        public static void SaveFile()
        {
            ConfigFile.Save(@"config.cfg");
        }

        public static string GetValue(string ConfigCategory, string Key)
        {
            return ConfigFile.GetValue(ConfigCategory, Key);
        }

        public static void SetValue(string ConfigCategory, string Key, string Value)
        {
            ConfigFile.SetValue(ConfigCategory, Key, Value);
            SaveFile();
        }
    }
}
