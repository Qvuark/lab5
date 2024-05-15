using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    [Serializable]
    public struct Train : IComparable<Train>
    {
        public string Destination { get; set; }
        public int NumberOfTrain { get; set; }
        public TimeSpan TimeOfLeaving { get; set; }

        public Train(string input)
        {
            string[] data = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Destination = data[0];
            NumberOfTrain = int.Parse(data[1]);
            TimeOfLeaving = TimeSpan.Parse(data[2]);
        }
        public int CompareTo(Train that)
        {
            return this.Destination.CompareTo(that.Destination);
        }
    }
}
