using System.IO;

namespace TechBot.Filesystem
{
    static class Files
    {
        public static void InitChannel(Objects.Channel Channel)
        {
            string ChannelFolder = Config.Folders.ChannelFolder;
            string ChannelLUA = ChannelFolder + Config.Folders.FolderSplit + Channel.Name.ToLower() + Config.Folders.FolderSplit + Config.LUA.DefaultFile;
            if (!File.Exists(ChannelLUA))
            {
                File.Copy(Config.Folders.TemplateFolder + Config.Folders.FolderSplit + Config.LUA.ChannelTemplate, ChannelLUA);
            }
        }
    }
}
