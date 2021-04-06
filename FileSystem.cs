using System;
using System.IO;

//This is not needed but only used to test on mac and windows

namespace FileSystem
{
    public class filesystem
    {
        public static bool isWin = true; //Checks if we are on windows or mac

        public string combineFile(string[] directs, string orgin). //used alot for filesystems
        {
            if (isWin == true)
            {
                string newpath = orgin;
                foreach (string direct in directs)
                {
                    newpath = newpath + @"\" + direct; //makes path for windows
                }
                return newpath; //return path

            } else
            {
                string newpath = orgin;
                foreach (string direct in directs)
                {
                    newpath = newpath + "/" + direct; //makes path for mac
                }
                return newpath; //returns path
            }
        }

        
    }

}
