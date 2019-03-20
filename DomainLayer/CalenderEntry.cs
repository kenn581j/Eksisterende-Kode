using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    // Enumerate values made to indicate what state an event object is in.
    public enum Status : byte
    {
        NewlyMade,
        Changed,
        Deleted,
        UnChanged
    }

    public class CalendarEntry
    {
        public int Key { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Hearse Hearse { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public Status Status { get; set; }


        // Constructor for our calendar entry.
        public CalendarEntry(int key, DateTime start, DateTime end, string address, string comment, Status status, Hearse hearse = null)
        {
            Key = key;
            Start = start;
            End = end;
            Address = address;
            Comment = comment;
            Status = status;
            Hearse = hearse;
        }
    }
}
