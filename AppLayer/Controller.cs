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
        private HearseRepo hearseRepository;
        private CalendarEntryRepo eventRepository;

        public Controller()
        {
            databaseController = new DatabaseController();
            HearseRepo hearseRepo = new HearseRepo();
            CalendarEntryRepo calendarEntryRepo = new CalendarEntryRepo(hearseRepo);         
        }


        public bool CreateEventType(bool reservation, string start, string end, string address, string comment)
        {            
            if (!DateTime.TryParse(start, out DateTime tstart))
            {
                return false;
            }
            if (!DateTime.TryParse(end, out DateTime tend))
            {
                return false;
            }
            return eventRepository.CreateEvent(tstart, tend, address, comment, reservation);
        }

        public bool AlterEvent(string key, string hearse, string start, string end, string address, string comment)
        {
            if (int.TryParse(key, out int ikey) && int.TryParse(hearse, out int ihearse))
            {
                if (start == "") { start = null; }
                if (end == "") { end = null; }
                if (address == "") { address = null; }
                if (comment == "") { comment = null; }

                return eventRepository.AlterEvent(ikey, start, end, address, comment, ihearse);
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
                return eventRepository.DeleteEvent(ikey);
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
                hearseRepository.AddHearse(hearse);
            }
            foreach (Tuple<int, DateTime, DateTime, int, string, string> item in databaseController.StartUpEvents())
            {
                Hearse hearse = hearseRepository.GetHearse(item.Item4);
                CalendarEntry events = new CalendarEntry(item.Item1, item.Item2, item.Item3, item.Item5, item.Item6, Status.UnChanged, hearse);
                eventRepository.AddEvent(events);
            }
        }

        public void Update()
        {
            databaseController.Update(eventRepository, hearseRepository);
        }
    }
}
