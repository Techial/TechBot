using System;
using System.Collections.Generic;
using System.Text;

namespace TechBot
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
        }

        public static void SendCommand(string Command)
        {
            IRC.client.SendRawMessage(Command);
        }
    }
}
