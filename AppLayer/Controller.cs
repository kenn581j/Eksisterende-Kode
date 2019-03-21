using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace AppLayer
{
    public class Controller
    {
        private DatabaseController databaseController;
        private HearseRepo hearseRepo;
        private CalendarEntryRepo eventRepo;

        public Controller()
        {
            databaseController = new DatabaseController();
            HearseRepo hearseRepo = new HearseRepo();
            CalendarEntryRepo calendarEntryRepo = new CalendarEntryRepo(hearseRepo);         
        }
        public string CreateEvent(DateTime eventStart, DateTime eventEnd, string address, string comment,bool hearseNeeded)
        {     
            /*
            //Both if's converts input to DateTime standard, if it fails it returns false
            if (!DateTime.TryParse(start, out DateTime timeStart))
            {
                return false;
            }
            
            if (!DateTime.TryParse(end, out DateTime timeEnd))
            {
                return false;
            }
            */
            //Calls eventRepo to create the event
            return eventRepo.CreateEvent(eventStart, eventEnd, address, comment, hearseNeeded);
        }

        public bool AlterEvent(int keyNo, int hearseNo, string start, string end, string address, string comment)
        {
            if (start == "") { start = null; }
            if (end == "") { end = null; }
            if (address == "") { address = null; }
            if (comment == "") { comment = null; }

            if (eventRepo.AlterEvent(keyNo, hearseNo, start, end, address, comment))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteEvent(string key)
        {
            if (int.TryParse(key, out int ikey))
            {
                return eventRepo.DeleteEvent(ikey);
            }
            else
            {
                return false;
            }
        }

        public void StartUp()
        {
            foreach (Tuple<int, int> item in databaseController.StartUpHearse())
            {
                Hearse hearse = new Hearse(item.Item2, item.Item1, Status.UnChanged);
                hearseRepo.AddHearse(hearse);
            }
            foreach (Tuple<int, DateTime, DateTime, int, string, string> item in databaseController.StartUpEvents())
            {
                Hearse hearse = hearseRepo.GetHearse(item.Item4);
                CalendarEntry events = new CalendarEntry(item.Item1, item.Item2, item.Item3, item.Item5, item.Item6, Status.UnChanged, hearse);
                eventRepo.AddEvent(events);
            }
        }

        public void Update()
        {
            databaseController.Update(eventRepo, hearseRepo);
        }
    }
}
