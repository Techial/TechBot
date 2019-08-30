using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TechBot.Objects
{
    public class Channel
    {
        ///<summary>
        ///Get Username/Channel name -> https://twitch.tv/username
        ///</summary>
        public string Name { get; private set; }
        ///<summary>
        ///Get current online users in Channel
        ///</summary>
        public List<User> Online { get; private set; } = new List<User>();

        ///<summary>
        ///LUA Container
        ///</summary>
        public LUA LUAContainer { get; private set; }

        ///<summary>
        ///IrcDotNet Channel Pointer
        ///</summary>
        public IrcDotNet.IrcChannel IrcChannelPointer { get; private set; }

        public User FindUser(string Username)
        {
            foreach (User user in Online)
            {
                if (user.Username == Username)
                {
                    return user;
                }
            }
            return null;
        }

        ///<summary>
        ///Constructor
        ///</summary>
        public Channel(IrcDotNet.IrcChannel NewChannel)
        {
            IrcChannelPointer = NewChannel;
            Name = NewChannel.Name;
            Filesystem.Folders.InitChannel(this);
            Filesystem.Files.InitChannel(this);
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
        private void RemoveUser(User user)
        {
            if (Online.Contains(user))
            {
                Online.Remove(user);
            }
        }

        ///<summary>
        ///Adds user to channel.<br></br>Ref AddUser()
        ///</summary>
        public void UserJoined(User user)
        {
            AddUser(user);
        }

        ///<summary>
        ///Removes user from channel.<br></br>Ref RemoveUser()
        ///</summary>
        public void UserLeft(User user)
        {
            RemoveUser(user);
        }
    }
}
