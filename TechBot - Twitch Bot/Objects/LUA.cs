using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace TechBot.Objects
{
    public class LUA
    {
        ///<summary>
        ///NLUA pointer for LUA environment.
        ///</summary>
        private NLua.Lua Environment;

        ///<summary>
        ///Modules
        ///</summary>
        public List<Module> Modules { get; private set; } = new List<Module>();

        ///<summary>
        ///Parent Channel
        ///</summary>
        public Channel ParentChannel { get; private set; }

        ///<summary>
        ///Load LUA Module
        ///</summary>
        public void LoadModule(Module module) {
            string ModuleName = module.Name;
            string Creator = module.Creator;

            string ModuleFile = "modules" + Filesystem.Folders.FolderSplit + Creator + Filesystem.Folders.FolderSplit + ModuleName + ".lua";

            if (File.Exists(ModuleFile))
                Environment.LoadFile(ModuleFile); // We should probably avoid loading the LUA files with LoadFile and instead make a command loading this into a sandbox?
                                                  // https://github.com/kikito/sandbox.lua/blob/master/sandbox.lua
        }

        public void RegisterLUAFunctions()
        {
            Environment.RegisterFunction("sendMessage", this, GetType().GetMethod("SendMessage"));

        }

        public void InitLUA(Channel Channel)
        {
            ParentChannel = Channel;
            string chName = Channel.Name;
            Environment = new NLua.Lua();

            //We should retrieve modules before looping through to load them.

            foreach (Module module in Modules)
            {
                LoadModule(module);
            }
            // Now you can start loading channel code.
            string channelLUA = "channels" + Filesystem.Folders.FolderSplit + chName + Filesystem.Folders.FolderSplit + Filesystem.Files.LUA_DefaultFile;

            if (File.Exists(channelLUA))
                Environment.LoadFile(channelLUA);// We should probably avoid loading the LUA files with LoadFile and instead make a command loading this into a sandbox?
                                                 // https://github.com/kikito/sandbox.lua/blob/master/sandbox.lua
        }

        ///<summary>
        ///Constructor
        ///</summary>
        public LUA(Channel Channel)
        {
            /*LUAThread = new Thread(() => {
                InitLUA(Channel);
            });
            LUAThread.Start();*/
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
            {
                InitLUA(Channel);
            }), null);
        }


        // ------------------------------------------REGISTERED FUNCTIONS----------------------------------------------
        public void SendMessage(string Message)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
            {
                IRC_Functions.SendMessage(ParentChannel, Message);
            }), null);
        }

        // -------------------------------------------------EVENTS-----------------------------------------------------
        public void ChatMessageReceived(User user,string Message)
        {
            // Should probably add an event handler, but RegisterLuaDelegateType is poorly documented.

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
            {
                NLua.LuaFunction MessageReceived = Environment["event_MessageReceived"] as NLua.LuaFunction;
                MessageReceived.Call(user.Username, Message); // Safer way to call than using DoString
            }), null);
        }

    }
}
