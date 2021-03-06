﻿namespace TechBot
{
    class IRC_Functions
    {
        public static void ClearUserMessages(string ChannelName, string UserName)
        {
            //:tmi.twitch.tv CLEARCHAT #[ChannelName] :[UserName]
            IRC.client.SendRawMessage(":tmi.twitch.tv CLEARCHAT #"+ChannelName.ToLower()+" :"+UserName);
        }

        public static void SendMessage(Objects.Channel Channel, string Message)
        {
            IRC.client.SendRawMessage("PRIVMSG #"+Channel.Name.ToLower()+" :"+Message);
            Log.Logger.OutputToConsole("[#{0}] {1}: {2}", Channel.Name.ToLower(),Config.Credentials.Username, Message);
        }

        public static void SendCommand(string Command)
        {
            IRC.client.SendRawMessage(Command);
        }
    }
}
