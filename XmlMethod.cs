using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return deserializedTrain;
        }
        public void Dispose()
        {

        }
    }
}
