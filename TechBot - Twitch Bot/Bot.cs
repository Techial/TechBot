using System;
using System.Threading;

namespace TechBot
{
    class Bot
    {
        static void Main(string[] args)
        {
            // Load config file (.ini) or create if it doesn't exist
            var ConfigFile = new Config.IniReader("settings.conf");

            // Check if default keys exist
            if (!ConfigFile.KeyExists("Username", "Credentials"))
                ConfigFile.Write("Username", "CHANGE THIS", "Credentials");

            if (!ConfigFile.KeyExists("OAuth", "Credentials"))
                ConfigFile.Write("Password", "CHANGE THIS", "Credentials");


            // Read Config values
            string Username = ConfigFile.Read("Username", "Credentials");
            string Password = ConfigFile.Read("OAuth", "Credentials");

            if (Username == "CHANGE THIS" || Password == "CHANGE THIS")
            {
                Console.WriteLine("Please change Username and/or OAuth key in TechBot.config");
                Console.WriteLine("Press any key to exit program ...");
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            // Connect Init(String["Username", "Password"])
            string[] Args = new string[2] { Username, Password };

            IRC.Init(Args);
        }
    }
}
