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
        //freaking long variables for locations in the csv files
        public filesystem FileSystem = new filesystem();
        public string[] brawlerList = new string[] { "SHELLY", "COLT", "BULL", "BROCK", "RICO", "SPIKE", "BARLEY", "JESSIE", "NITA", "DYNAMIKE", "ELPRIMO", "MORTIS", "CROW", "POCO", "BOW", "PIPER", "PAM", "TARA", "DARRYL", "PENNY", "FRANK", "GENE", "TICK", "LEON", "ROSA", "CARL", "BIBI", "8BIT", "SANDY", "BEA", "EMZ", "MRP", "MAX", "EMPTY", "JACKY", "GALE", "NANI", "SPROUT" };
        public int[] brawllistint = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 51, 53, 55, 59, 61, 74, 76, 78, 80, 81, 85, 90, 91, 0, 97, 99, 101, 103 };
        public int[] apkbrawllistint = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 51, 53, 55, 59, 61, 64, 66, 68, 70, 71, 73, 75, 77, 0, 83, 85, 87, 89};

        public int healthcolumn = 9;
        public int speedcolumn = 8;
        public int AttackDPBcolumn = 17;

        public List<Modification> mods = new List<Modification> { };
        public List<ImportedMap> maps = new List<ImportedMap> { };

        public string[] ModeOptions = new string[] { "Bounty", "BrawlBallArena", "Showdown", "Gemgrab", "Heist", "Siege" };
        public string[] MapRanges = new string[] { "36:68", "744:776", "333:392", "135:167", "234:266", "2935:2973" };


        public class Modification //mod class made when you change a brawler
        {
            public string brawler;
            public string modval;
            public string value;

            public Modification(string brawler, string modval, string value)
            {
                this.brawler = brawler;
                this.modval = modval;
                this.value = value;
                
            }
        }

        public class ImportedMap //map class when you import a map
        {
            public string[] lines;
            public string mode;
            public ImportedMap(string[] lines, string mode)
            {
                this.lines = lines;
                this.mode = mode;
                  
            }
        }

        public void WriteOnLine(int row, int column, string newValue, string filepath) //function for writing to csv
        {
            string givenPath;
            if (Program.isipa == true)
            {
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "Payload", "Payload","Brawl Stars.app", "res" }, "") + filepath;

            }
            else
            {
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104", "assets" }, "") + filepath;

            }

            string[] filetext = File.ReadAllLines(givenPath);
            string[] filetextLine = filetext[row - 1].Split(',');
            filetextLine[column - 1] = newValue;
            string newlinetext = string.Join(',', filetextLine);
            filetext[row - 1] = newlinetext;
            File.WriteAllLines(givenPath, filetext);
        }

        public string ReadOnLine(int row, int column, string filepath) //function to read on csv
        {
            string givenPath;
            if (Program.isipa == true)
            {
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] {"Payload","Payload","Brawl Stars.app", "res"}, "") + filepath;

            }
            else
            {
                givenPath = Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104", "assets"}, "") + filepath;

            }

            string[] filetext = File.ReadAllLines(givenPath);

            string[] filetextLine = filetext[row - 1].Split(',');


            return filetextLine[column - 1];
        }

        public void Startmessage() //main menu
        {

            //Website for getting latest verison
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://brawlmodderversion.000webhostapp.com/");

            string webData = System.Text.Encoding.UTF8.GetString(raw);
            string macversionline = webData.Split("<p>")[1];
            string winversionline = webData.Split("<p>")[2];
            string macversion = macversionline.Split("</p>")[0].Split(":")[1];
            string winversion = winversionline.Split("</p>")[0].Split(":")[1];
            //Website for getting latest version
            string version = File.ReadAllLines(FileSystem.combineFile(new string[] { "bmversion"}, Directory.GetCurrentDirectory()))[0].Split(":")[1];
            Console.WriteLine("      Welcome to Brawl Modder Windows Beta 2");
            Console.WriteLine("   Type the number of the actions you want to do");
            Console.WriteLine("1 -- Create IPA");
            Console.WriteLine("2 -- Create APK");
            Console.WriteLine("3 -- Edit File");
            if (version == winversion) //checks for update
            {
                Console.WriteLine("       No updates");

            } else
            {
                Console.WriteLine("4--Update Avalible");

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
                return true;
             
            } else
            {
                if (input.Split().Length > 1)
                {
                    if (input.Split()[0] == "read") //shows the brawler data
                    {
                        if (brawlerList.Contains(input.Split()[1].ToUpper()))
                        {
                            string brawler = input.Split()[1];
                            string damage;
                            if (Program.isipa == true)
                            {
                                damage = ReadOnLine(brawllistint[Array.IndexOf(brawlerList, input.Split()[1].ToUpper())], AttackDPBcolumn, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                            } else {

                                damage = ReadOnLine(apkbrawllistint[Array.IndexOf(brawlerList, input.Split()[1].ToUpper())], AttackDPBcolumn, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                            }
                            string health = ReadOnLine(Array.IndexOf(brawlerList, input.Split()[1].ToUpper()) + 2, healthcolumn, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));
                            string speed = ReadOnLine(Array.IndexOf(brawlerList, input.Split()[1].ToUpper()) + 2, speedcolumn, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));


                            Console.WriteLine($"The brawler {brawler} does {damage} damage, has {health} health, and goes {speed} speed.");
                            Thread.Sleep(1000);
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("That is not a brawler");
                            Thread.Sleep(1000);
                            return false;
                        }
                    } else
                    {
                        if (brawlerList.Contains(input.Split()[0].ToUpper())) // adds modification by making class
                        {
                            mods.Add(new Modification(input.Split()[0].ToUpper(), input.Split()[1], input.Split()[2]));
                            Console.WriteLine("Added modification");
                            Thread.Sleep(1000);
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("That is not a brawler");
                            Thread.Sleep(1000);
                            return false;
                        }
                    }
                    
                } else
                {
                    Console.WriteLine("You need three arguments");
                    Thread.Sleep(1000);
                    return false;
                }
            }
            
        }

        public bool MainMessageAction(string input) //actiond for main message
        {
            if (input == "1") //quit
            {
                return true;
            }
            else if (input == "2") //importing map
            {
                Console.WriteLine("What is the file path of your map");
                Console.WriteLine("_________________");
                string path = Console.ReadLine();

                if (File.Exists(path))
                {

                    if (Path.GetExtension(path) == ".txt") //seperates title and apps a imported map class
                    {
                        string[] lines = File.ReadAllLines(path);
                        string mode = lines[0].Split("|")[2];
                        if (ModeOptions.Contains(mode))
                        {

                            maps.Add(new ImportedMap(lines, mode));
                            
                        }
                        else
                        {
                            Console.WriteLine("We do not support this mode yet");
                        }
                    }
                    else
                    {
                        Console.WriteLine("That is not a txt file");
                    }
                }
                else
                {
                    Console.WriteLine("That is not a file");
                }
            

                return false;
            }
            else if (input == "3") //saving changes
            {

                foreach (ImportedMap item in maps.ToArray()) //each map it parses and adds to csv
                {
                    int index = 1;
                    for (int i = int.Parse(MapRanges[Array.IndexOf(ModeOptions, item.mode)].Split(":")[0]); i < int.Parse(MapRanges[Array.IndexOf(ModeOptions, item.mode)].Split(":")[1]) + 1; i += 1)
                    {
                        WriteOnLine(i, 3, item.lines[index], FileSystem.combineFile(new string[] {"csv_logic", "maps.csv"}, ""));
                        index += 1;
                    }
                    Console.WriteLine("Saved a Map");
                }

                foreach (Modification item in mods.ToArray()) //each mod in brawler mods it parses and adds to csv
                {
                    if (item.modval == "s")
                    {
                        WriteOnLine(Array.IndexOf(brawlerList, item.brawler) + 3, speedcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));

                    }
                    else if (item.modval == "h")
                    {
                        WriteOnLine(Array.IndexOf(brawlerList, item.brawler) + 3, healthcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "characters.csv" }, ""));

                    }
                    else if (item.modval == "d")
                    {
                        if (Program.isipa == true)
                        {
                            WriteOnLine(brawllistint[Array.IndexOf(brawlerList, item.brawler)], AttackDPBcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                        }
                        else
                        {
                            WriteOnLine(apkbrawllistint[Array.IndexOf(brawlerList, item.brawler)], AttackDPBcolumn, item.value, FileSystem.combineFile(new string[] { "csv_logic", "skills.csv" }, ""));

                        }

                    }
                    else
                    {
                        Console.WriteLine("Something went wrong");
                    }
                    Console.WriteLine($"{item.brawler} now has {item.value}, {item.modval}");

                }
                Thread.Sleep(1000);

                Console.WriteLine("Would you like to export? (y/n)"); //exporting
                Console.WriteLine("______________________________");
                string output = Console.ReadLine();
                if (output == "y")
                {
                    Console.WriteLine("What do you want the file name to be?");
                    Console.WriteLine("_____________________________________");
                    string filename = Console.ReadLine();
                    Console.WriteLine("What file path will you export it to?");
                    Console.WriteLine("_____________________________________");
                    string exportpath = Console.ReadLine();
                    if (Program.isipa == true) //zips in either ipa or apk
                    {
                        ZipFile.CreateFromDirectory(FileSystem.combineFile(new string[] { "Payload" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { $"{filename}.ipa" }, exportpath));

                    }
                    else
                    {
                        ZipFile.CreateFromDirectory(FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { $"{filename}.apk" }, exportpath));

                    }
                    Console.WriteLine("Moved Item");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
                return false;
            }
            else
            {
                Console.WriteLine("That is not an option");
                Thread.Sleep(1000);
                return false;
            }
            
        }

        public void download(string link, string name) //just download function
        {
            using (WebClient Client = new WebClient())
            {
                Client.DownloadFile(link, name);
            }
        }

        public bool StartMessageAction(string input) //main menu actions
        {
            if (input == "1") //setup for ipa
            {
                string newpath = FileSystem.combineFile(new string[] {"rebrawl-classic-66.ipa"}, Directory.GetCurrentDirectory());
                File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory()));
                Program.isipa = true;
                ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "brawlstars.zip" }, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "Payload" }, ""));
                return true;
            } else
            {
                if (input == "2") //setup for apk
                {
                    string newpath = FileSystem.combineFile(new string[] { "rebrawl-classic-104.apk" }, Directory.GetCurrentDirectory());
                    File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory()));
                    Program.isipa = false;
                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "brawlstars.zip" }, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, ""));
                    return true;
                } else
                {
                    if (input == "3") // choosing a file for modding
                    {
                        Console.WriteLine("_____________________");
                        Console.WriteLine("What is the file name");
                        Console.WriteLine("_____________________");
                        string newpath = Console.ReadLine();
                        if (File.Exists(newpath))
                        {
                            if (Path.GetExtension(newpath) == ".ipa" || Path.GetExtension(newpath) == ".apk")
                            {

                                if (Path.GetExtension(newpath) == ".ipa")
                                {
                                    File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory()));
                                    Program.isipa = true;
                                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] {"brawlstars.zip"}, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "Payload" }, ""));


                                }
                                else
                                {
                                    File.Copy(newpath, FileSystem.combineFile(new string[] { "brawlstars.zip" }, Directory.GetCurrentDirectory()));
                                    Program.isipa = false;
                                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "brawlstars.zip" }, ""), Directory.GetCurrentDirectory() + FileSystem.combineFile(new string[] { "rebrawl-classic-104" }, ""));


                                }
                                return true;
                            } else
                            {
                                Console.WriteLine("That file is not a ipa or an apk");
                                Thread.Sleep(1000);
                                return false;
                            }
                        } else
                        {
                            Console.WriteLine("That path does not exist");
                            Thread.Sleep(1000);
                            return false;
                        }
                        
                    }
                    else
                    {
                        if (input == "4") {

                            System.Net.WebClient wc = new System.Net.WebClient(); //downloads the latest version from my server
                            byte[] raw = wc.DownloadData("https://brawlmodderversion.000webhostapp.com/");

                            string webData = System.Text.Encoding.UTF8.GetString(raw);
                            string line = webData.Split("<p>")[4];
                            string linkdata = line.Split("</p>")[0].Split("@")[1];
                            Console.WriteLine("What file path will you export it to?"); //exports new version
                            Console.WriteLine("_____________________________________");
                            string exportpath = Console.ReadLine();
                            download("https://brawlmodderversion.000webhostapp.com/BrawlModderWin.zip", "BrawlModderWin.zip");
                            ZipFile.ExtractToDirectory(FileSystem.combineFile(new string[] {"BrawlModderWin.zip"}, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "BrawlModder" }, Directory.GetCurrentDirectory()));
                            File.Copy(FileSystem.combineFile(new string[] { "rebrawl-classic-66.ipa" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "BrawlModder", "net5.0", "rebrawl-classic-66.ipa" }, Directory.GetCurrentDirectory()));
                            File.Copy(FileSystem.combineFile(new string[] { "rebrawl-classic-104.apk" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "BrawlModder", "net5.0", "rebrawl-classic-104.apk" }, Directory.GetCurrentDirectory()));
                            Directory.Move(FileSystem.combineFile(new string[] { "BrawlModder", "net5.0" }, Directory.GetCurrentDirectory()), FileSystem.combineFile(new string[] { "net5.0" }, exportpath));
                            Directory.Delete(FileSystem.combineFile(new string[] { "BrawlModder" }, Directory.GetCurrentDirectory()), true);
                            File.Delete(FileSystem.combineFile(new string[] { "BrawlModderWin.zip" }, Directory.GetCurrentDirectory()));

                            Console.WriteLine("New update is in given path");
                            Thread.Sleep(1000);


                            
                            return false;

                        } else
                        {
                            Console.WriteLine("That is not an option");
                            Thread.Sleep(1000);
                            return false;
                        }
                        
                    }
                }
            }
        }

    }
}
