using Salaros.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TechBot.Filesystem
{
    class Folders
    {
        public static string FolderSplit = "/";

        // DYNAMIC (THESE VALUES WILL BE CHANGED BY INIT)
        public static string ChannelFolder = "";
        public static string ModuleFolder = "";
        public static string TemplateFolder = "";

        public static void Init()
        {
            // Set Config Category we're working in
            string ConfigCategory = "Structure";

            // Load config file (.ini) or create if it doesn't exist
            var ConfigFile = new ConfigParser(@"settings.conf");

            // Read config values
            ChannelFolder = ConfigFile.GetValue(ConfigCategory, "ChannelFolder");
            ModuleFolder = ConfigFile.GetValue(ConfigCategory, "ModuleFolder");
            TemplateFolder = ConfigFile.GetValue(ConfigCategory, "TemplateFolder");

            // Check if values are empty and fill as needed
            if (String.IsNullOrEmpty(ChannelFolder))
            {
                string value = "Channels";
                ConfigFile.SetValue(ConfigCategory, "ChannelFolder", value);

                ChannelFolder = value;
            }

            if (String.IsNullOrEmpty(ModuleFolder))
            {
                string value = "Modules";
                ConfigFile.SetValue(ConfigCategory, "ModuleFolder", value);

                ModuleFolder = value;
            }

            if (String.IsNullOrEmpty(TemplateFolder))
            {
                string value = "Templates";
                ConfigFile.SetValue(ConfigCategory, "TemplateFolder", value);

                TemplateFolder = value;
            }

            if (!Directory.Exists(ChannelFolder))
                Directory.CreateDirectory(ChannelFolder);

            if (!Directory.Exists(ModuleFolder))
                Directory.CreateDirectory(ModuleFolder);

            if (!Directory.Exists(TemplateFolder))
                Directory.CreateDirectory(TemplateFolder);
        }

        public static void InitChannel(Objects.Channel Channel)
        {
            string ChannelDestination = ChannelFolder + Folders.FolderSplit + Channel.Name;
            if (!Directory.Exists(ChannelDestination))
                Directory.CreateDirectory(ChannelDestination);
        }
    }
}
