using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TechBot.Objects
{
    class LUA
    {
        ///<summary>
        ///NLUA pointer for LUA environment.
        ///</summary>
        private NLua.Lua Environment;

        ///<summary>
        ///Modules
        ///</summary>
        public List<Module> Modules {get; private set;}

        ///<summary>
        ///Load LUA Module
        ///</summary>
        public void LoadModule(Module module) {
            string ModuleName = module.Name;
            string Creator = module.Creator;

            string ModuleFile = "modules/" + Creator + "/" + ModuleName + ".lua";

            if (File.Exists(ModuleFile))
                Environment.LoadFile(ModuleFile); // We should probably avoid loading the LUA files with LoadFile and instead make a command loading this into a sandbox?
                                                  // https://github.com/kikito/sandbox.lua/blob/master/sandbox.lua
        }

        ///<summary>
        ///Constructor
        ///</summary>
        public LUA(Channel Channel)
        {
            string chName = Channel.Name;
            Environment = new NLua.Lua();

            //We should retrieve modules before looping through to load them.

            foreach(Module module in Modules) {
                LoadModule(module);
            }
            // Now you can start loading channel code.
            string channelLUA = "channels/" + chName + "/main.lua";

            if (File.Exists(channelLUA))
                Environment.LoadFile(channelLUA);// We should probably avoid loading the LUA files with LoadFile and instead make a command loading this into a sandbox?
                                                 // https://github.com/kikito/sandbox.lua/blob/master/sandbox.lua
        }

    }
}
