using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab
{
    partial class TxtMethod
    {
        internal static void WriteTrainIntoTxt(List<Train> train, string filename)
        {
            StreamWriter sw = new(filename);
            foreach (var item in train)
            {
                sw.WriteLine($"{item.Destination} {item.NumberOfTrain} {item.TimeOfLeaving}");
            }
            sw.Close();
        }
        internal static List<Train> ReadTrainFromTxt(string filename)
        {
            string line;
            List<Train> train = new();
            StreamReader sr = new(filename);
            line = sr.ReadLine();
            while (line != null)
            {
                train.Add(new Train(line));
                line = sr.ReadLine();
            }
            sr.Close();
            Console.WriteLine("Deserialized from txt:");
            lab5.PrintStruct(train);
            return train;
        }

    }
}