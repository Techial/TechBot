using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Salaros.Configuration;

namespace TechBot.Filesystem
{
    class Files
    {
        public static string LUA_ChannelTemplate = "";
        public static string Github_URL = "https://github.com/Techial/TechBot/";
        public static string LUA_DefaultFile = "";
        public static void Init()
        {
            // Set Config Category we're working in
            string ConfigCategory = "Files";

            // Load config file (.ini) or create if it doesn't exist
            var ConfigFile = new ConfigParser(@"settings.conf");

            // Read config values
            LUA_ChannelTemplate = ConfigFile.GetValue(ConfigCategory, "LUA_ChannelTemplate");
            LUA_DefaultFile = ConfigFile.GetValue(ConfigCategory, "LUA_DefaultFile");

            // Check if values are empty and fill as needed
            if (String.IsNullOrEmpty(LUA_ChannelTemplate))
            {
                string value = "Templates" + Folders.FolderSplit + "Channel.lua";
                ConfigFile.SetValue(ConfigCategory, "LUA_ChannelTemplate", value);

                LUA_ChannelTemplate = value;
            }

            if (String.IsNullOrEmpty(LUA_DefaultFile))
            {
                string value = "main.lua";
                ConfigFile.SetValue(ConfigCategory, "LUA_DefaultFile", value);

                LUA_DefaultFile = value;
            }

            if(!File.Exists(LUA_ChannelTemplate))
            {
                using (WebClient wc = new WebClient())
                    wc.DownloadFile(Github_URL+"Templates/Channel.lua",LUA_ChannelTemplate); // Download Channel-template from Github page
                // Should probably add a version control to update this file??
                // Or leave it up to the end user??
            }
        }

        public static void InitChannel(Objects.Channel Channel)
        {
            string ChannelFolder = Folders.ChannelFolder;
            string ChannelLUA = ChannelFolder + Folders.FolderSplit + Channel.Name + Folders.FolderSplit + LUA_DefaultFile;
            if (!File.Exists(ChannelLUA))
            {
                File.Copy(LUA_ChannelTemplate, ChannelLUA);
            }
        }
    }
}
