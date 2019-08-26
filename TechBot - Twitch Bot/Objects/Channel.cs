using System;
using System.Collections.Generic;
using System.Text;

namespace TechBot.Objects
{
    class Channel
    {
        ///<summary>
        ///Get Username/Channel name -> https://twitch.tv/username
        ///</summary>
        public string Name { get; private set; }
        ///<summary>
        ///Get current online users in Channel
        ///</summary>
        public List<User> Online { get; private set; }

        ///<summary>
        ///LUA Container
        ///</summary>
        public LUA LUAContainer { get; private set; }

        ///<summary>
        ///Constructor
        ///</summary>
        public Channel(string ChannelName)
        {
            Name = ChannelName;
            LUAContainer = new LUA(this);
        }

        ///<summary>
        ///Add user to Channel
        ///</summary>
        private void AddUser(User user)
        {
            if(!Online.Contains(user))
            {
                Online.Add(user);
            }
        }

        ///<summary>
        ///Removes user from Channel
        ///</summary>
        public void RemoveUser(User user)
        {
            if (!Online.Contains(user))
            {
                Online.Remove(user);
            }
        }

        ///<summary>
        ///Adds user to channel.<br></br>Ref AddUser()
        ///</summary>
        private void UserJoined(User user)
        {
            AddUser(user);
        }

        ///<summary>
        ///Removes user from channel.<br></br>Ref RemoveUser()
        ///</summary>
        private void UserLeft(User user)
        {
            RemoveUser(user);
        }
    }
}
