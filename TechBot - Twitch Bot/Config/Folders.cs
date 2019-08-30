using System;

namespace TechBot.Config
{
    static class Folders
    {
        private static string ConfigCategory { get; } = "Folders";
        public static string FolderSplit = "/";

        // DYNAMIC (THESE VALUES WILL BE CHANGED BY INIT)
        public static string ChannelFolder = "";
        public static string ModuleFolder = "";
        public static string TemplateFolder = "";

        public static void Init()
        {
            // Read config values
            Config.Folders.ChannelFolder = Config.Folders.GetValue("Channel");
            Config.Folders.ModuleFolder = Config.Folders.GetValue("Module");
            Config.Folders.TemplateFolder = Config.Folders.GetValue("Template");

            // Check if values are empty and fill as needed
            if (String.IsNullOrEmpty(Config.Folders.ChannelFolder))
            {
                string value = "Channels";
                Config.Folders.SetValue("Channel", value);

                Config.Folders.ChannelFolder = value;
            }

            if (String.IsNullOrEmpty(Config.Folders.ModuleFolder))
            {
                string value = "Modules";
                Config.Folders.SetValue("Module", value);

                Config.Folders.ModuleFolder = value;
            }

            if (String.IsNullOrEmpty(Config.Folders.TemplateFolder))
            {
                string value = "Templates";
                Config.Folders.SetValue("Template", value);

                Config.Folders.TemplateFolder = value;
            }

            if (!System.IO.Directory.Exists(Config.Folders.ChannelFolder))
                System.IO.Directory.CreateDirectory(Config.Folders.ChannelFolder);

            if (!System.IO.Directory.Exists(Config.Folders.ModuleFolder))
                System.IO.Directory.CreateDirectory(Config.Folders.ModuleFolder);

            if (!System.IO.Directory.Exists(Config.Folders.TemplateFolder))
                System.IO.Directory.CreateDirectory(Config.Folders.TemplateFolder);
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
