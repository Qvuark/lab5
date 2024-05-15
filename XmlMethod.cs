using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace lab
{
    public class XmlMethod : IDisposable
    {
        internal static void SerializeToXml(List<Train> train, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Train>));
            using (TextWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, train);
            }
            Console.WriteLine("Serialized in Xml");
            lab5.PrintStruct(train);
        }
        internal static List<Train> DeserializeTrainFromXml(string filename)
        {
            List<Train> deserializedTrain = new();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Train>));
            FileStream filestream = new FileStream(filename, FileMode.Open);
            deserializedTrain = (List<Train>)serializer.Deserialize(filestream);
            filestream.Close();
            Console.WriteLine("Deserialized in Xml");
            lab5.PrintStruct(deserializedTrain);
            return deserializedTrain;
        }
        public static List<Train> XmlReaderInFile(TimeSpan time, string filename)
        {
            List<Train> trains = new List<Train>();
            using (XmlReader reader = XmlReader.Create(filename))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name == "Train")
                    {
                        reader.ReadToFollowing("TimeOfLeaving");
                        if (!string.IsNullOrEmpty(reader.Value))
                        {
                            TimeSpan trainTime = XmlConvert.ToTimeSpan(reader.Value);
                            if (trainTime == time)
                            {
                                reader.ReadToFollowing("Destination");
                                string destination = reader.ReadString();
                                reader.ReadToFollowing("NumberOfTrain");
                                int numberOfTrain = reader.ReadElementContentAsInt();
                                string temp = ($"{destination} {numberOfTrain} {trainTime}");
                                Train train = new Train(temp);
                                trains.Add(train);
                            }
                        }
                    }
                }
            }
            return trains;
        }

        public void Dispose()
        {

        }
    }
}
