using Salaros.Configuration;
using System;

namespace TechBot.Config
{
    static class Channel
    {
        private static string ConfigCategory { get; } = "Config";
        public static string GetCommandPrefix(Objects.Channel channel)
        {
            string ConfigPath = Config.Folders.ChannelFolder + Config.Folders.FolderSplit + channel.Name + Config.Folders.FolderSplit + "config.cfg";
            ConfigParser ConfigFile = new ConfigParser(ConfigPath);
            string Prefix = ConfigFile.GetValue(ConfigCategory, "Command_Prefix");
            if (String.IsNullOrEmpty(Prefix))
            {
                ConfigFile.SetValue(ConfigCategory, "Command_Prefix",Config.LUA.CommandPrefix);
                Prefix = Config.LUA.CommandPrefix; // Default
            }

            ConfigFile.Save(ConfigPath);

            return Prefix;
        }
    }
}
