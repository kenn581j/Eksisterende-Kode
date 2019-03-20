using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    // Hearse class to make our hearse objects used in the calendar to check availabilty.
    public class Hearse
    {
        public int Key { get; set; }
        public int Priority { get; set; }
        public Status Status { get; set; }

        // Hearse constructor with two parameters.
        public Hearse(int prio, Status sta)
        {
            Priority = prio;
            Status = sta;
        }

        // Hearse constructor with three parameters.
        public Hearse(int key, int prio, Status sta) : this(prio, sta)
        {
            Key = key;
        }

        // A simple override for the Equals function.
        public override bool Equals(object obj)
        {
            if (obj is Hearse)
            {
                return this.Priority == (obj as Hearse).Priority; 
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
