using Salaros.Configuration;
using System;
using System.Threading;

namespace TechBot
{
    class Bot
    {
        static void Main(string[] args)
        {
            // Set Config Category we're working in
            string ConfigCategory = "Credentials";

            // Load config file (.ini) or create if it doesn't exist
            var ConfigFile = new ConfigParser(@"settings.conf");

            // Read config values
            string Username = ConfigFile.GetValue(ConfigCategory, "Username");
            string Password = ConfigFile.GetValue(ConfigCategory, "OAuth");

            // Check if values are empty and fill as needed
            if (String.IsNullOrEmpty(Username))
                ConfigFile.SetValue(ConfigCategory, "Username", "CHANGE THIS");

            if (String.IsNullOrEmpty(Password))
                ConfigFile.SetValue(ConfigCategory, "OAuth", "CHANGE THIS");
            

            if (Username == "CHANGE THIS" || Password == "CHANGE THIS" || String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                Console.WriteLine("Please change Username and/or OAuth in TechBot.config");
                Console.WriteLine("https://twitchapps.com/tmi/ for generating OAuth token (Copy whole textbox)");
                Console.WriteLine("Press any key to exit program ...");
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            // Connect Init(String["Username", "Password"])
            string[] Args = new string[2] { Username, Password };

            //Init Filesystem first
            Filesystem.Folders.Init();
            Filesystem.Files.Init();

            //Now we can connect to IRC
            IRC.Init(Args);
        }
    }
}
