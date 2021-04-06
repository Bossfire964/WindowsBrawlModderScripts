using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using functions;
using FileSystem;

namespace _5._0WindowsBrawlModder
{
    public class Program
    {
        public static Functions function = new Functions(); //functions connection
        public static string input;
        public static bool isipa = true;
        static void Main(string[] args)
        {
            filesystem FileSystem = new filesystem(); //all of this deletes any leftover files from last session
            if (Directory.Exists(FileSystem.combineFile(new string[] {"Payload"}, Directory.GetCurrentDirectory()))) {
                Directory.Delete(FileSystem.combineFile(new string[] { "Payload" }, Directory.GetCurrentDirectory()), true);
            }
            if (Directory.Exists(FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, Directory.GetCurrentDirectory())))
            {
                Directory.Delete(FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, Directory.GetCurrentDirectory()), true);
            }
            if (File.Exists(FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory())))
            {
                File.Delete(FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory()));
            }
            bool answer = false;
            while (answer == false) //main menu
            {
                function.Startmessage();
                input = Console.ReadLine();
                answer = function.StartMessageAction(input);
            }

            bool biganswer = false;
            
            while (biganswer == false) //main loop for editing
            {
                answer = false;
                while (answer == false) //brawler edit or import map options
                {
                    function.MainMessage();
                    input = Console.ReadLine();
                    answer = function.MainMessageAction(input);
                }
                answer = false;
                while (answer == false) // brawler edit screen
                {
                    function.BrawlEditMessage();
                    input = Console.ReadLine();
                    answer = function.BrawlEditAction(input);
                }
            }
            
            
            
            
        }
    }
}
