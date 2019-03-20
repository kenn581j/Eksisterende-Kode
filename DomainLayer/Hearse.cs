using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Hearse
    {
        public int Key { get; set; }
        public int Priority { get; set; }
        public Status Status { get; set; }

        // Hearse constructor with two parameters.
        public Hearse(int prio, Status status) : 
            this(0, prio, status)
        {
        }

        // Hearse constructor with three parameters.
        public Hearse(int key, int prio, Status status)
        {
            Key = key;
            Priority = prio;
            Status = status;
        }
    }
}
