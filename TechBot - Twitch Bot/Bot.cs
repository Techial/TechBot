using System;

namespace TechBot
{
    class Bot
    {
        static void Main(string[] args)
        {
            // Init Credentials config
            Config.Credentials.Init();

            if (Config.Credentials.Username == "CHANGE THIS" || Config.Credentials.Password == "CHANGE THIS")
            {
                Log.Logger.OutputToConsole("Please change Username and/or OAuth in TechBot.config");
                Log.Logger.OutputToConsole("https://twitchapps.com/tmi/ for generating OAuth token (Copy whole textbox)");
                Log.Logger.OutputToConsole("Press any key to exit program ...");
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            // Connect Init(String["Username", "Password"])
            string[] Args = new string[2] { Config.Credentials.Username, Config.Credentials.Password };

            //Init Configs
            Config.Folders.Init();
            Config.Files.Init();
            Config.LUA.Init();

            //Now we can connect to IRC
            IRC.Init(Args);
        }
    }
}
