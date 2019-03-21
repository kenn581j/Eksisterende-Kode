using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Hearse
    {
        public int Id { get; set; }
        /*
        public int Priority { get; set; }
        public Status Status { get; set; }

        // Hearse constructor with two parameters.
        public Hearse(int prio, Status status) : 
            this(0, prio, status)
        {
        }
        */
        public Hearse(int id)
        {
            Id = id;
        }
    }
}
