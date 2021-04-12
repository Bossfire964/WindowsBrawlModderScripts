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
            if (isWin == true) //checks if it is for windows
            {
                string newpath = orgin; //sets the starting path at the givin orgin
                foreach (string direct in directs) //for each attaching file
                {
                    newpath = newpath + @"\" + direct; //makes path for windows with forward slash
                }
                return newpath; //return path

            } else //if for mac
            {
                string newpath = orgin; //sets starting path at the given orgin
                foreach (string direct in directs) //foreach attaching file
                {
                    newpath = newpath + "/" + direct; //makes path for mac with back slash 
                }
                return newpath; //returns path
            }
        }

        
    }

}
