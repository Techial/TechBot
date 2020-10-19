using System;

namespace TechBot.Config
{
    static class Credentials
    {
        public static string Username = "";
        public static string Password = "";
        private static string ConfigCategory { get; } = "Credentials";

        public static void Init()
        {
            // Read config values
            Username = GetValue("Username");
            Password = GetValue("OAuth");

            // Check if values are empty and fill as needed
            if (String.IsNullOrEmpty(Username))
            {
                string value = "CHANGE THIS";
                SetValue("Username", value);

                Username = value;
            }

            if (String.IsNullOrEmpty(Password))
            {
                string value = "CHANGE THIS";
                SetValue("OAuth", value);

                Password = value;
            }
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
