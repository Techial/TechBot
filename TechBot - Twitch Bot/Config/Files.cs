namespace TechBot.Config
{
    static class Files
    {
        public static string WebProtocol = "https://";
        public static string Github_URL = "github.com/Techial/TechBot/";
        private static string ConfigCategory { get; } = "Files";

        public static void Init()
        {
            // Nothing for now
        }

        public static string GetValue(string Key)
        {
            return MainConfigFile.GetValue(ConfigCategory, Key);
        }

        public static void SetValue(string Key, string Value)
        {
            MainConfigFile.SetValue(ConfigCategory, Key, Value);
            MainConfigFile.SaveFile();
        }
    }
}
