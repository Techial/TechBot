namespace TechBot.Objects
{
    public class User
    {
        ///<summary>
        ///Get Username -> https://twitch.tv/username
        ///</summary>
        public string Username { get; private set; }

        public string ChannelName { get; private set; }

        ///<summary>
        ///Constructor
        ///</summary>
        public User(string User, string ChName)
        {
            Username = User;
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
