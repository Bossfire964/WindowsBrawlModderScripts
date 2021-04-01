using System;
using System.IO;


namespace FileSystem
{
    public class filesystem
    {
        public static bool isWin = true;

        public string combineFile(string[] directs, string orgin)
        {
            if (isWin == true)
            {
                string newpath = orgin;
                foreach (string direct in directs)
                {
                    newpath = newpath + @"\" + direct;
                }
                return newpath;

            } else
            {
                string newpath = orgin;
                foreach (string direct in directs)
                {
                    newpath = newpath + "/" + direct;
                }
                return newpath;
            }
        }

        
    }

}