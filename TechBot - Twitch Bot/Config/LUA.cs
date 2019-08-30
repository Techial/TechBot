using System;
using System.Net;

namespace TechBot.Config
{
    static class LUA
    {
        public static string DefaultFile = "";
        public static string ChannelTemplate = "";
        public static string CommandPrefix = "";
        private static string ConfigCategory { get; } = "LUA";

        public static void Init()
        {
            // Read config values
            ChannelTemplate = GetValue("ChannelTemplate");
            DefaultFile = GetValue("DefaultFile");
            CommandPrefix = GetValue("Command_Prefix");

            // Check if values are empty and fill as needed
            if (String.IsNullOrEmpty(ChannelTemplate))
            {
                string value = "Templates" + Folders.FolderSplit + "Channel.lua";
                SetValue("ChannelTemplate", value);

                ChannelTemplate = value;
            }

            if (String.IsNullOrEmpty(DefaultFile))
            {
                string value = "main.lua";
                SetValue("DefaultFile", value);

                DefaultFile = value;
            }

            if (String.IsNullOrEmpty(CommandPrefix))
            {
                string value = "!";
                SetValue("Command_Prefix", value);

                CommandPrefix = value;
            }

            if (!System.IO.File.Exists(Config.LUA.ChannelTemplate))
            {
                using (WebClient wc = new WebClient())
                    wc.DownloadFile(Config.Files.WebProtocol + "raw." + Config.Files.Github_URL + "master/Templates/Channel.lua", ChannelTemplate); // Download Channel-template from Github page
                // Should probably add a version control to update this file??
                // Or leave it up to the end user??
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
