using System;
using System.Collections.Generic;
using System.Text;

namespace TechBot.Objects
{
    public class User
    {
        ///<summary>
        ///Get Username -> https://twitch.tv/username
        ///</summary>
        public string Username { get; private set; }

        ///<summary>
        ///Check if user is channel moderator.
        ///</summary>
        public Boolean IsMod { get; private set; }

        ///<summary>
        ///Pointer to IrcDotNet Client
        ///</summary>
        public IrcDotNet.IrcUser IRCClient { get; private set; }

        public string ChannelName { get; private set; }

        ///<summary>
        ///Constructor
        ///</summary>
        public User(IrcDotNet.IrcUser Client, string ChName)
        {
            IRCClient = Client;
            Username = IRCClient.NickName;
            ChannelName = ChName;
        }

        ///<summary>
        ///Kick user from Channel
        ///</summary>
        public void KickUser()
        {
            // /kick <username>
        }

        ///<summary>
        ///Ban user from Channel
        ///</summary>
        public void BanUser(int Time)
        {
            if (Time <= 0)
            {
                // /ban <username>
            } else {
                // /timeout <username> <seconds>
            }
            //TODO
        }

        ///<summary>
        ///Clear messages by user in Channel
        ///</summary>
        public void ClearMessages()
        {
            IRC_Functions.ClearUserMessages(ChannelName, Username);
        }
    }
}
