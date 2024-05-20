using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Xml.Linq;

namespace lab
{
    partial class lab5
    {
        public static void FindTrainsByTime(TimeSpan time, string name)
        {
            if (!File.Exists(name))
            {
                Console.WriteLine("File does not exist");
                return;
            }
            List<Train> trains = new List<Train>();
            List<Train> trainsSorted = new List<Train>();
            if (name == "Train.txt")
            {
                using (StreamReader sr = new StreamReader(name))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Train train = new Train(line);
                        if (train.TimeOfLeaving == time)
                        {
                            trains.Add(train);
                        }
                    }
                }
                PrintStruct(trains);
            }
            else if (name == "Train.xml")
            {
                trains = XmlMethod.DeserializeTrainFromXml(name);
                trainsSorted = trains.FindAll(train => train.TimeOfLeaving == time);
                if (trainsSorted.Count== 0)
                    Console.WriteLine("There are no trains in this time" +
                        "");
                PrintStruct(trainsSorted);
            } 
            
        }
        public static void PrintStruct(List<Train> trains)
        {
            foreach (var item in trains)
            {
                Console.WriteLine($"{item.Destination} {item.NumberOfTrain} {item.TimeOfLeaving}");
            }
        }
        static List<Train> WriteStructDataToList()
        {
            List<Train> train = new List<Train>();
            if (File.Exists("Train.txt") || File.Exists("Train.xml"))
            {
                Console.WriteLine("Input file already exists. What do you want to do with him? Print \"add\" to add new information, print \"rewrite\" to rewrite whole file and write anything to just show list of trains");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "add":
                        {
                            if (File.Exists("Train.txt"))
                                train = TxtMethod.ReadTrainFromTxt("Train.txt");
                            if (File.Exists("Train.xml"))
                                train = XmlMethod.DeserializeTrainFromXml("Train.xml");
                            PrintStruct(train);
                            Console.WriteLine("Do you want to add something?(y/n)");
                            char addOrNot = char.Parse(Console.ReadLine());
                            if (addOrNot == 'y')
                            {
                                Console.WriteLine("Enter data");
                                string input1 = Console.ReadLine();
                                while (input1 != "stop")
                                {
                                    train.Add(new Train(input1));
                                    input1 = Console.ReadLine();
                                }
                                train.Sort();
                            }
                            WriteStruckToFile(train);
                            break;
                        }
                    case "rewrite":
                        {
                            train.Clear(); // Clear existing data
                            Console.WriteLine("Enter new data (type 'stop' to finish):");
                            string input = Console.ReadLine();
                            while (input.ToLower() != "stop")
                            {
                                train.Add(new Train(input));
                                input = Console.ReadLine();
                            }
                            train.Sort();
                            WriteStruckToFile(train);
                            break;
                        }
                    case "xml":
                        {
                            if (File.Exists("Train.xml"))
                            {
                                train = XmlMethod.DeserializeTrainFromXml("Train.xml");
                                PrintStruct(train);
                            }
                            else
                            {
                                Console.WriteLine("File does not exist");
                            }
                            break;
                        }
                    case "txt":
                        {
                            if (File.Exists("Train.txt"))
                            {
                                train = TxtMethod.ReadTrainFromTxt("Train.txt");
                                PrintStruct(train);
                            }
                            else
                            {
                                Console.WriteLine("File does not exist");
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }
            }
            return train;
        }

        public static void WriteStruckToFile(List<Train> train)
        {
            Console.WriteLine("Print 1 for xml, 2 for txt, 3 for both");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    {
                        string filename = "Train.xml";
                        XmlMethod.SerializeToXml(train, filename);
                        PrintStruct(train);
                        break;
                    }
                case "2":
                    {
                        string filename = "Train.txt";
                        TxtMethod.WriteTrainIntoTxt(train, filename);
                        PrintStruct(train);
                        break;
                    }
                case "3":
                    {
                        string filename1 = "Train.xml";
                        string filename2 = "Train.txt";
                        XmlMethod.SerializeToXml(train, filename1);
                        TxtMethod.WriteTrainIntoTxt(train, filename2);
                        PrintStruct(train);
                        break;
                    }
            }
        }
        static void ReadFile()
        {
            Console.WriteLine("Write 1 to run xml file or 2 for txt method");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        string filename = "Train.xml";
                        if (!File.Exists("Train.xml")) { Console.WriteLine("File does not exist"); break; }
                        XmlMethod.DeserializeTrainFromXml(filename);
                        Console.WriteLine("Enter time you want to check:");
                        TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                        FindTrainsByTime(time, filename);
                        break;
                    }
                case 2:
                    {
                        string filename = "Train.txt";
                        if (!File.Exists("Train.txt")) { Console.WriteLine("File does not exist"); break; }
                        TxtMethod.ReadTrainFromTxt(filename);
                        Console.WriteLine("Enter time you want to check:");
                        TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                        FindTrainsByTime(time, filename);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong input");
                        break;
                    }
            }
        }
        static void Main()
        {
            Console.WriteLine("Print 1 to read file or 2 to make changes in file");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    ReadFile();
                    break;
                case 2:
                    WriteStructDataToList();
                    break;
                default:
                    {
                        Console.WriteLine("Wrong input");
                        break;
                    }
            }
        }

    }

}