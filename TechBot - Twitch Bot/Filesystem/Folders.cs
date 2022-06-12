using Salaros.Configuration;
using System;
using System.IO;

namespace TechBot.Filesystem
{
    static class Folders
    {
        public static void InitChannel(Objects.Channel Channel)
        {
            string ChannelDestination = Config.Folders.ChannelFolder + Config.Folders.FolderSplit + Channel.Name.ToLower();
            if (!Directory.Exists(ChannelDestination))
                Directory.CreateDirectory(ChannelDestination);
        }
    }
}
