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
        public static Functions function = new Functions(); //functions connection for main actions
        public static string input; //main input of the file, public to everyone
        public static bool isipa = true; //if file is ipa or apk, avalible for everyone
        static void Main(string[] args) //main function
        {
            filesystem FileSystem = new filesystem(); //connects to the filesystem
            if (Directory.Exists(FileSystem.combineFile(new string[] {"Payload"}, Directory.GetCurrentDirectory()))) { //if Payload folder exists
                Directory.Delete(FileSystem.combineFile(new string[] { "Payload" }, Directory.GetCurrentDirectory()), true); //deletes Payload
            }
            if (Directory.Exists(FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, Directory.GetCurrentDirectory()))) //if rebrawl-classic-104 exists
            {
                Directory.Delete(FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, Directory.GetCurrentDirectory()), true); //deletes rebrawl-classic-104
            }
            if (File.Exists(FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory()))) //if brawlstars.zip exists
            {
                File.Delete(FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory())); //deletes brawlstars.zip
            }
            bool answer = false; //variable for while loop
            while (answer == false) //main menu loop
            {
                function.Startmessage(); //prints message
                input = Console.ReadLine(); //reads input
                answer = function.StartMessageAction(input); // connects to action and sets answer to true or false
            }

            bool biganswer = false; //loop variable for main editing
            
            while (biganswer == false) //main loop for editing
            {
                answer = false; //variable for mini loop for brawler edits and importing maps
                while (answer == false) //brawler edit or import map options
                {
                    function.MainMessage(); //displays message 
                    input = Console.ReadLine();  //reads input
                    answer = function.MainMessageAction(input);// does action and sets answer to either true or false
                }
                answer = false; //variable for mini loop brawler edit
                while (answer == false) // brawler edit screen
                {
                    function.BrawlEditMessage(); //displays message 
                    input = Console.ReadLine(); //reads input
                    answer = function.BrawlEditAction(input); //does action and sets answer to true or false
                }
            }
            
            
            
            
        }
    }
}
