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
            List<Train> trains = new List<Train>();
            string filename = name;
            if (!File.Exists(filename))
            {
                Console.WriteLine("File does not exist");
            }
            else
            {
                if(filename == "Train.txt")
                {
                    using (StreamReader sr = new StreamReader(filename))
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
                    Print(trains);
                }
                if(filename=="Train.xml")
                {
                    List<Train> trains1 = XmlMethod.XmlReaderInFile(time, filename);
                    Print(trains1);
                }
            }
        }
        public static void Print(List<Train> trains)
        {
            foreach (Train train in trains)
            {
                Console.WriteLine($"{train.Destination} {train.NumberOfTrain} {train.TimeOfLeaving}");
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
            if(File.Exists("Train.txt")|| File.Exists("Train.xml"))
            {
                Console.WriteLine("Input file already exists. Do you want to read it?");
                char choice = char.Parse(Console.ReadLine());
                if(choice == 't')
                {
                    if (File.Exists("Train.txt"))
                        train = TxtMethod.ReadTrainFromTxt("Train.txt");
                    if (File.Exists("Train.xml"))
                        train = XmlMethod.DeserializeTrainFromXml("Train.xml");
                    Console.WriteLine("Do you want to add anything?");
                    char choice2 = char.Parse(Console.ReadLine());
                    if(choice2 == 't')
                    {
                        train.Sort((a,b)=>a.CompareTo(b));
                        Console.WriteLine("Structure:");
                        PrintStruct(train);
                        return train;
                    }
                }
                else { Console.WriteLine("Dont read anything"); }
            }
            Console.WriteLine("Enter data");
            string input = Console.ReadLine();
            while (input != "stop")
            {
                train.Add(new Train(input));
                input = Console.ReadLine();
            }
            train.Sort();
            return train;
        }
        public static void WriteStruckToFile()
        {
            List<Train> train = WriteStructDataToList();
            Console.WriteLine("1 for xml, 2 for txt, 3 for both");
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
            Console.WriteLine("Write 1 to run xml file or 2 to run txt method");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        string filename = "Train.xml";
                        if (!File.Exists("Train.xml")) { Console.WriteLine("file do not exist"); break; }
                        XmlMethod.DeserializeTrainFromXml(filename);
                        Console.WriteLine("Enter time you want to check:");
                        TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                        FindTrainsByTime(time, filename);
                        break;
                    }
                case 2:
                    {
                        string filename = "Train.txt";
                        if (!File.Exists("Train.txt")) { Console.WriteLine("file do not exist"); break; }
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
            Console.WriteLine("1 to readfile 2 to write file");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    ReadFile();
                    break;
                case 2:
                    WriteStruckToFile();
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