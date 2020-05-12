using System;
using System.Collections.Generic;
using System.Text;

namespace TechBot.Log
{
    static class Logger
    {
        public static void OutputToConsole(params string[] args)
        {
            string[] formats = new string[args.Length - 1];

            int i = 0;
            foreach(string f in args)
            {
                if (i>0)
                {
                    formats[i-1] = @f;
                }
                i++;
            }
            string message = string.Format(args[0], formats);
            Console.WriteLine(@message);
        }
    }
}
