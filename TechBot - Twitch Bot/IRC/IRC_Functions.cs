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
            IRC.client.SendRawMessage(":tmi.twitch.tv CLEARCHAT #"+ChannelName+" :"+UserName);
        }
    }
}
