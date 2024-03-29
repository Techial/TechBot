﻿using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net;
using System;

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

            string ModuleFile = Config.Folders.ModuleFolder + Config.Folders.FolderSplit + Creator + Config.Folders.FolderSplit + ModuleName + ".lua";

            if (File.Exists(ModuleFile))
                Environment.DoFile(ModuleFile); // We should probably avoid loading the LUA files with LoadFile and instead make a command loading this into a sandbox?
                                                // https://github.com/kikito/sandbox.lua/blob/master/sandbox.lua
        }

        public void RegisterLUAFunctions()
        {
            Environment.RegisterFunction("sendMessage", this, GetType().GetMethod("SendMessage"));
            Environment.RegisterFunction("downloadString", this, GetType().GetMethod("DownloadString"));

            // DEBUG
            Environment.RegisterFunction("printToConsole", this, GetType().GetMethod("PrintToConsole"));
        }

        public void RefreshLUA()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
            {
                InitLUA(ParentChannel);
                IRC_Functions.SendMessage(ParentChannel, "Script refreshed.");
            }), null);
        }

        public void InitLUA(Channel Channel)
        {
            ParentChannel = Channel;
            string chName = Channel.Name;
            Environment = new NLua.Lua();

            RegisterLUAFunctions();

            //We should retrieve modules before looping through to load them.

            foreach (Module module in Modules)
            {
                try
                {
                    LoadModule(module);
                } catch
                {
                    // Do nothing for now
                }
            }
            // Now you can start loading channel code.
            string channelLUA = Config.Folders.ChannelFolder + Config.Folders.FolderSplit + chName.ToLower() + Config.Folders.FolderSplit + Config.LUA.DefaultFile;

            try
            {
                if (File.Exists(channelLUA))
                {
                    Environment.DoFile(channelLUA);// We should probably avoid loading the LUA files with LoadFile and instead make a command loading this into a sandbox?
                                                   // https://github.com/kikito/sandbox.lua/blob/master/sandbox.lua
                } else
                {
                    Log.Logger.OutputToConsole("Could not find "+channelLUA);
                }
            } catch (Exception e)
            {
                Log.Logger.OutputToConsole("Script failed " + e.ToString());
            }
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

        public string DownloadString(string Message)
        {
            WebClient web = new WebClient();
            string result = web.DownloadString(Message);
            return result;
        }

        public void PrintToConsole(string Message)
        {
            Log.Logger.OutputToConsole(@Message);
        }

        // -------------------------------------------------EVENTS-----------------------------------------------------
        public void CommandReceived(User user, bool IsMod, string Message)
        {
            // Should probably add an event handler, but RegisterLuaDelegateType is poorly documented.

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
            {
                try
                {
                    List<string> strlist = new List<string>(Message.Split(" "));
                    if(Environment["command_"+strlist[0].Substring(1)] == null)
                    {
                        //IRC_Functions.SendMessage(ParentChannel, "Command not found.");
                        return;
                    }

                    NLua.LuaFunction MessageReceived = Environment["command_"+strlist[0].Substring(1)] as NLua.LuaFunction;
                    strlist.RemoveAt(0);
                    MessageReceived.Call(user.Username, IsMod, strlist); // Safer way to call than using DoString
                } catch (NLua.Exceptions.LuaScriptException e)
                {
                    //IRC_Functions.SendMessage(ParentChannel, "Script failed");
                    Log.Logger.OutputToConsole(e.ToString());
                } catch (Exception e) {
                    Log.Logger.OutputToConsole("Script failed " + e.ToString());
                }
            }), null);
        }

        public void ChatMessageReceived(User user, bool IsMod, string Message)
        {
            // Should probably add an event handler, but RegisterLuaDelegateType is poorly documented.

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
            {
                try
                {
                    NLua.LuaFunction MessageReceived = this.Environment.GetFunction("event_MessageReceived");
                    //NLua.LuaFunction MessageReceived = this.Environment["event_MessageReceived"] as NLua.LuaFunction;
                    MessageReceived.Call(user.Username, IsMod, Message); // Safer way to call than using DoString
                } catch (NLua.Exceptions.LuaScriptException e)
                {
                    IRC_Functions.SendMessage(ParentChannel, "Script failed");
                    Log.Logger.OutputToConsole(e.ToString());
                } catch (Exception e) {
                    Log.Logger.OutputToConsole("Script failed "+e.ToString());
                }
            }), null);
        }

    }
}
