using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;


namespace AppLayer
{
    public class HearseRepo
    {
        private List<Hearse> hearseList = new List<Hearse>();

        public void AddHearse(Hearse hearse)
        {
            hearseList.Add(hearse);
        }
        
        public void CreateHearse(int prio, Status status)
        {

            // For every hearse in the hearse list do ...
            foreach (Hearse i in hearseList)
            {
                if (prio == i.Priority)
                {
                    throw new MemberAccessException();
                }
            }
            hearseList.Add(new Hearse(prio, status));
        }


        // Alter a hearse object that takes two parameters
        public void AlterHearse(int prio, int cpri)
        {
            foreach (Hearse i in hearseList)
            {
                if (prio == i.Priority)
                {
                    i.Priority = cpri;
                }
            }
        }


        // Creates hearse at startup
        public void StartUpHearse(int prio)
        {
            hearseList.Add(new Hearse(prio, Status.UnChanged));
        }


        // Deletes a hearse of the prio key parameter
        public void DeleteHearse(int prio)
        {
            foreach (Hearse i in hearseList)
            {
                if (prio == i.Priority)
                {
                    i.Status = Status.Deleted;
                }
            }
        }


        // Method for retrieving a hearse.
        public Hearse GetHearse(int key)
        {
            foreach (Hearse hearse in hearseList)
            {
                if (hearse.Key == key)
                {
                    return hearse;
                }
            }
            return null;
        }
        
        public List<Hearse> GetListOfHearses()
        {
            List<Hearse> list = new List<Hearse>();
            foreach (Hearse item in hearseList)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
