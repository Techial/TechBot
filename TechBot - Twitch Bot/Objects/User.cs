using System;
using System.Collections.Generic;
using System.Text;

namespace TechBot.Objects
{
    class User
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
        ///Constructor
        ///</summary>
        public User(string User)
        {
            Username = User;
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
    }
}
