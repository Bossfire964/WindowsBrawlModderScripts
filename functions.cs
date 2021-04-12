using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using FileSystem;
using System.Net;
using System.Linq;
using _5._0WindowsBrawlModder;


namespace functions
{
    public class Functions
    {
        public filesystem FileSystem = new filesystem(); //connections for the filesystem
        //brawler list in order in files vv
        public string[] brawlerList = new string[] { "SHELLY", "COLT", "BULL", "BROCK", "RICO", "SPIKE", "BARLEY", "JESSIE", "NITA", "DYNAMIKE", "ELPRIMO", "MORTIS", "CROW", "POCO", "BOW", "PIPER", "PAM", "TARA", "DARRYL", "PENNY", "FRANK", "GENE", "TICK", "LEON", "ROSA", "CARL", "BIBI", "8BIT", "SANDY", "BEA", "EMZ", "MRP", "MAX", "EMPTY", "JACKY", "GALE", "NANI", "SPROUT" };
        //brawler index for rows in csv files vv
        public int[] brawllistint = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 51, 53, 55, 59, 61, 74, 76, 78, 80, 81, 85, 90, 91, 0, 97, 99, 101, 103 };
        //brawler index for rows in csv files for apk vv
        public int[] apkbrawllistint = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 51, 53, 55, 59, 61, 64, 66, 68, 70, 71, 73, 75, 77, 0, 83, 85, 87, 89};

        public int healthcolumn = 9; //what column brawler health is in 
        public int speedcolumn = 8; //what column brawler speed is in
        public int AttackDPBcolumn = 17; //what column brawler attack is in

        public List<Modification> mods = new List<Modification> { }; //list of all brawler mods classes
        public List<ImportedMap> maps = new List<ImportedMap> { }; //list if all imported map classes

        public string[] ModeOptions = new string[] { "Bounty", "BrawlBallArena", "Showdown", "Gemgrab", "Heist", "Siege" }; //map game mods avalible
        public string[] MapRanges = new string[] { "36:68", "744:776", "333:392", "135:167", "234:266", "2935:2973" }; // ranges for each game mode row in the files


        public class Modification //mod class made when you change a brawler
        {
            public string brawler; //brawler name
            public string modval; //brawler mod types
            public string value; //brawler mod value

            public Modification(string brawler, string modval, string value) //sets up mod
            {
                this.brawler = brawler; //sets brawler name
                this.modval = modval; //sets brawler mode type
                this.value = value; //sets brawler mod value
                
            }
        }

        public class ImportedMap //map class when you import a map
        {
            public string[] lines; //lines of the map
            public string mode; //mode of the map
            public ImportedMap(string[] lines, string mode) //sets up map 
            {
                this.lines = lines; //sets the lines of the maps
                this.mode = mode; //sets the mode of the map
                  
            }
        }

        public void WriteOnLine(int row, int column, string newValue, string filepath) //function for writing to csv
        {
            string givenPath; //path to the csv
            if (Program.isipa == true) //if it is a ipa
            {
                //makes path for the csv
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "Payload", "Payload","Brawl Stars.app", "res" }, "") + filepath;

            }
            else //for apk
            {
                //makes path for csv
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104", "assets" }, "") + filepath;

            }

            string[] filetext = File.ReadAllLines(givenPath); //reads all lines of the file
            string[] filetextLine = filetext[row - 1].Split(','); //makes array of selected row
            filetextLine[column - 1] = newValue; //sets column value to given value
            string newlinetext = string.Join(',', filetextLine); //variable for new row
            filetext[row - 1] = newlinetext; //sets new row into lines
            File.WriteAllLines(givenPath, filetext); //writes into file
        }

        public string ReadOnLine(int row, int column, string filepath) //function to read on csv
        {
            string givenPath; //varible for path to csv
            if (Program.isipa == true) //if files is ipa
            {
                //path to csv
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] {"Payload","Payload","Brawl Stars.app", "res"}, "") + filepath;

            }
            else //if file is apk
            {
                //path to csv
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104", "assets"}, "") + filepath;

            }

            string[] filetext = File.ReadAllLines(givenPath); //reads all lines

            string[] filetextLine = filetext[row - 1].Split(','); //array of selected column


            return filetextLine[column - 1]; //returns given column
        }

        public void Startmessage() //main menu
        {

            //Website for getting latest verison
            System.Net.WebClient wc = new System.Net.WebClient(); //makes web client
            byte[] raw = wc.DownloadData("https://brawlmodderversion.000webhostapp.com/"); //gets website data

            string webData = System.Text.Encoding.UTF8.GetString(raw); //decodes it into text
            string macversionline = webData.Split("<p>")[1]; //makes a line by splitting text by <p>
            string winversionline = webData.Split("<p>")[2]; //makes line by splitting text by <p>
            string macversion = macversionline.Split("</p>")[0].Split(":")[1]; //gets version number from website for mac
            string winversion = winversionline.Split("</p>")[0].Split(":")[1]; //gets version number from website for windows 
            //Website for getting latest version
            string version = File.ReadAllLines(FileSystem.combineFile(new string[] { "bmversion"}, Directory.GetCurrentDirectory()))[0].Split(":")[1]; //gets current version from bmversion file
            Console.WriteLine("      Welcome to Brawl Modder Windows Beta 2");
            Console.WriteLine("   Type the number of the actions you want to do");
            Console.WriteLine("1 -- Create IPA");
            Console.WriteLine("2 -- Create APK");
            Console.WriteLine("3 -- Edit File");
            if (version == winversion) //checks if website value and bmversion vakue are the same
            {
                Console.WriteLine("       No updates"); //show no updates

            } else //not the same
            {
                Console.WriteLine("4--Update Avalible"); //show update is avalible

            }
            Console.WriteLine("_________________");


        }

        public void MainMessage() //main message
        {
            Console.WriteLine("    Type your command below");
            Console.WriteLine(" Type one of the following actions");
            Console.WriteLine("1 -- Edit Brawler");
            Console.WriteLine("2 -- Import Map");
            Console.WriteLine("3 -- Save Edits");
            Console.WriteLine("___________________");




        }


        public void BrawlEditMessage() //brawler edit message
        {
            Console.WriteLine("    Type the following command to edit a brawler");
            Console.WriteLine("  Or type Q to exit");
            Console.WriteLine("Type read <brawler> to get their data");
            Console.WriteLine("Command is <brawler> <d/s/h> <value>");
            Console.WriteLine("d = Damage");
            Console.WriteLine("s = Speed");
            Console.WriteLine("h = Health");
            Console.WriteLine("_________________");

        }


        public bool BrawlEditAction(string input) // actions when you edit brawler
        {
            if (input == "q") //quit
            {
                return true; //stops loop
             
            } else
            {
                if (input.Split().Length > 1) //if not 1 word
                {
                    if (input.Split()[0] == "read") //if first word is read
                    {
                        if (brawlerList.Contains(input.Split()[1].ToUpper())) //if brawler given is brawler
                        {
                            string brawler = input.Split()[1]; //varible to hold brawler
                            string damage; //damages variable
                            if (Program.isipa == true) //if file is ipa
                            {
                                //gets damages for ipa file
                                damage = ReadOnLine(brawllistint[Array.IndexOf(brawlerList, input.Split()[1].ToUpper())], AttackDPBcolumn, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                            } else { //if file is apk

                                //gets damages for apk file
                                damage = ReadOnLine(apkbrawllistint[Array.IndexOf(brawlerList, input.Split()[1].ToUpper())], AttackDPBcolumn, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                            }
                            //gets brawler health
                            string health = ReadOnLine(Array.IndexOf(brawlerList, input.Split()[1].ToUpper()) + 2, healthcolumn, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));
                            //gets brawler speed
                            string speed = ReadOnLine(Array.IndexOf(brawlerList, input.Split()[1].ToUpper()) + 2, speedcolumn, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));


                            Console.WriteLine($"The brawler {brawler} does {damage} damage, has {health} health, and goes {speed} speed."); //dislpays all the info together
                            Thread.Sleep(1000); //waits one second
                            return false; //continues the loop
                        }
                        else //input is not a brawler
                        {
                            Console.WriteLine("That is not a brawler"); //not a brawler
                            Thread.Sleep(1000); //wait one second
                            return false; //continue the loop
                        }
                    } else //word one is not read
                    {
                        if (brawlerList.Contains(input.Split()[0].ToUpper())) //if word one is a brawler
                        {
                            mods.Add(new Modification(input.Split()[0].ToUpper(), input.Split()[1], input.Split()[2])); //makes a mod class with the other inputs
                            Console.WriteLine("Added modification"); //displays message
                            Thread.Sleep(1000); //waits one second
                            return false; //continues the loop
                        }
                        else //brawler does not exist
                        {
                            Console.WriteLine("That is not a brawler"); //dispays message
                            Thread.Sleep(1000); //waits ones second
                            return false; //continues the loop
                        }
                    }
                    
                } else //message is 1 word or less
                {
                    Console.WriteLine("You need three arguments"); // displays message
                    Thread.Sleep(1000); //wait one second
                    return false; //continue loop
                }
            }
            
        }

        public bool MainMessageAction(string input) //action for main message
        {
            if (input == "1") //quit
            {
                return true; //stops the loop
            }
            else if (input == "2") //importing map
            {
                Console.WriteLine("What is the file path of your map"); //asks file path
                Console.WriteLine("_________________");
                string path = Console.ReadLine(); //gets input for file path 

                if (File.Exists(path)) //if the given path exists 
                {

                    if (Path.GetExtension(path) == ".txt") //if file is a txt
                    {
                        string[] lines = File.ReadAllLines(path); //reads lines of txt
                        string mode = lines[0].Split("|")[2]; //gets mode from first line
                        if (ModeOptions.Contains(mode)) //if mode is supported
                        {

                            maps.Add(new ImportedMap(lines, mode)); //add imported maps class
                            
                        }
                        else //mods is not supported 
                        {
                            Console.WriteLine("We do not support this mode yet"); //displays message
                        }
                    }
                    else //file is not txt
                    {
                        Console.WriteLine("That is not a txt file"); //displays message
                    }
                }
                else //files does not exists 
                {
                    Console.WriteLine("That is not a file"); //displays message
                }
            

                return false; //continues loop
            }
            else if (input == "3") //saving changes
            {

                foreach (ImportedMap item in maps.ToArray()) //for each map mod in the map list
                {
                    int index = 1; //index
                    //for each line in the map mode range
                    for (int i = int.Parse(MapRanges[Array.IndexOf(ModeOptions, item.mode)].Split(":")[0]); i < int.Parse(MapRanges[Array.IndexOf(ModeOptions, item.mode)].Split(":")[1]) + 1; i += 1)
                    {
                        WriteOnLine(i, 3, item.lines[index], FileSystem.combineFile(new string[] {"csv_logic", "maps.csv"}, "")); //writes the lines according the the imported map
                        index += 1; //changes index
                    }
                    Console.WriteLine("Saved a Map"); //dislpays message
                }

                foreach (Modification item in mods.ToArray()) //for each brawler mod class in list
                {
                    if (item.modval == "s") //if speed is changing
                    {
                       //changes speed
                        WriteOnLine(Array.IndexOf(brawlerList, item.brawler) + 3, speedcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));

                    }
                    else if (item.modval == "h") //if health is changing
                    {
                        //changes health
                        WriteOnLine(Array.IndexOf(brawlerList, item.brawler) + 3, healthcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));

                    }
                    else if (item.modval == "d") //if damages is changing
                    {
                        if (Program.isipa == true) //is file is ipa
                        {
                            //changes damage for ipa
                            WriteOnLine(brawllistint[Array.IndexOf(brawlerList, item.brawler)], AttackDPBcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                        }
                        else //if file is apk
                        {
                            //changes damage for apk
                            WriteOnLine(apkbrawllistint[Array.IndexOf(brawlerList, item.brawler)], AttackDPBcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                        }

                    }
                    else //mod value is not any of these for some reason
                    {
                        Console.WriteLine("Something went wrong"); //display message
                    }
                    Console.WriteLine($"{item.brawler} now has {item.value}, {item.modval}"); //displays message confirming changes 

                }
                Thread.Sleep(1000); //wait one second

                Console.WriteLine("Would you like to export? (y/n)"); //displays messsage
                Console.WriteLine("______________________________");
                string output = Console.ReadLine(); //reads input
                if (output == "y") //if input is yes
                {
                    Console.WriteLine("What do you want the file name to be?"); //displays message
                    Console.WriteLine("_____________________________________");
                    string filename = Console.ReadLine(); //reads input as filename
                    Console.WriteLine("What file path will you export it to?"); //displays message
                    Console.WriteLine("_____________________________________");
                    string exportpath = Console.ReadLine(); //reads input as exportpath
                    if (Program.isipa == true) //if file is ipa
                    {
                        //makes zip file from system and renames to ipa file
                        ZipFile.CreateFromDirectory(FileSystem.combineFile(new string[] { "Payload" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { $"{filename}.ipa" }, exportpath));

                    }
                    else //if file is apk
                    {
                        //makes zip file from system and renames it to apk file
                        ZipFile.CreateFromDirectory(FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { $"{filename}.apk" }, exportpath));

                    }
                    Console.WriteLine("Moved Item"); //displays message
                    Thread.Sleep(1000); //wait one second
                    Environment.Exit(0); //quits application
                }
                return false; //continues loop if you dedcided not to export
            }
            else //input is not a option in menu
            {
                Console.WriteLine("That is not an option"); //displays message
                Thread.Sleep(1000); //wait one second
                return false; // continues the loop
            }
            
        }

        public void download(string link, string name) //download function
        {
            using (WebClient Client = new WebClient()) //using web client
            {
                Client.DownloadFile(link, name); //downloads the link and saved as the name given
            }
        }

        public bool StartMessageAction(string input) //main menu actions
        {
            if (input == "1") //setup for ipa
            {
                string newpath = FileSystem.combineFile(new string[] {"rebrawl-classic-66.ipa"}, Directory.GetCurrentDirectory()); //makes path for ipa
                File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory())); //renames file to zip file
                Program.isipa = true; //sets the universal variable true
                //extracts the zip files contents
                ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "brawlstars.zip" }, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "Payload" }, ""));
                return true; //stops the loop
            } else
            {
                if (input == "2") //setup for apk
                {
                    string newpath = FileSystem.combineFile(new string[] { "rebrawl-classic-104.apk" }, Directory.GetCurrentDirectory()); //makes path for apk
                    File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory())); //renames apk to zip
                    Program.isipa = false; //sets universal variable to false for is an ipa
                    //extacts the zip files contents
                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "brawlstars.zip" }, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, ""));
                    return true; //stops the loop
                } else
                {
                    if (input == "3") // choosing a file for modding
                    {
                        Console.WriteLine("_____________________");
                        Console.WriteLine("What is the file name"); //dislpays message
                        Console.WriteLine("_____________________");
                        string newpath = Console.ReadLine(); //reads input and sest variable new path
                        if (File.Exists(newpath)) //if path exists
                        {
                            if (Path.GetExtension(newpath) == ".ipa" || Path.GetExtension(newpath) == ".apk") // if file is a apk or ipa
                            {

                                if (Path.GetExtension(newpath) == ".ipa") //if file is ipa
                                {
                                    File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory())); //make a copy and rename it to zip
                                    Program.isipa = true; //set universal varible to true
                                    //extract zip contents
                                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] {"brawlstars.zip"}, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "Payload" }, ""));


                                }
                                else //if file is apk
                                {
                                    File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory())); //makes a copy of the file and renames it to a zip
                                    Program.isipa = false; //sets universal variable to false
                                    //extracts the contents of the zip file
                                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "brawlstars.zip" }, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, ""));


                                }
                                return true; //stops the loop
                            } else //files is not apk or ipa
                            {
                                Console.WriteLine("That file is not a ipa or an apk"); //displays message
                                Thread.Sleep(1000); //waits one second
                                return false; //continues loop
                            }
                        } else //given path does not exist
                        {
                            Console.WriteLine("That path does not exist"); //displays message
                            Thread.Sleep(1000); //waits one second
                            return false; //continues the loop
                        }
                        
                    }
                    else
                    {
                        if (input == "4") { //downloads latest version from my server

                            //System.Net.WebClient wc = new System.Net.WebClient(); 
                            //byte[] raw = wc.DownloadData("https://brawlmodderversion.000webhostapp.com/"); 

                            //string webData = System.Text.Encoding.UTF8.GetString(raw); //makes it into text
                            //string line = webData.Split("<p>")[4];
                            //string linkdata = line.Split("</p>")[0].Split("@")[1];
                            
                            
                            Console.WriteLine("What file path will you export it to?"); //displays message
                            Console.WriteLine("_____________________________________");
                            string exportpath = Console.ReadLine(); //gets input for exporting path
                            download("https://brawlmodderversion.000webhostapp.com/BrawlModderWin.zip", "BrawlModderWin.zip"); //downloads the file with my function
                            //extracts the zip file you downladed
                            ZipFile.ExtractToDirectory(FileSystem.combineFile(new string[] {"BrawlModderWin.zip"}, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "BrawlModder" }, Directory.GetCurrentDirectory()));
                            //copies ipa file to new file
                            File.Copy(FileSystem.combineFile(new string[] { "rebrawl-classic-66.ipa" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "BrawlModder", "net5.0", "rebrawl-classic-66.ipa" }, Directory.GetCurrentDirectory()));
                            //copies apk file to new copy
                            File.Copy(FileSystem.combineFile(new string[] { "rebrawl-classic-104.apk" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "BrawlModder", "net5.0", "rebrawl-classic-104.apk" }, Directory.GetCurrentDirectory()));
                            //moves the app out of the extracted folder to given export location
                            Directory.Move(FileSystem.combineFile(new string[] { "BrawlModder", "net5.0" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "net5.0" }, exportpath));
                            //deletes the extracted folder
                            Directory.Delete(FileSystem.combineFile(new string[] { "BrawlModder" }, Directory.GetCurrentDirectory()), true);
                            //deletes original zip download
                            File.Delete(FileSystem.combineFile(new string[] { "BrawlModderWin.zip" }, Directory.GetCurrentDirectory()));

                            Console.WriteLine("New update is in given path"); //displays messgae
                            Thread.Sleep(1000); //waits one second


                            
                            return false; //continues the loop

                        } else //input is not a given option
                        {
                            Console.WriteLine("That is not an option"); //displays message
                            Thread.Sleep(1000); //waits one second
                            return false; //continues the loop
                        }
                        
                    }
                }
            }
        }

    }
}
