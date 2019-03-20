using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace AppLayer
{
    public class CalendarEntryRepo
    {
        private List<CalendarEntry> Eventslist;
        private HearseRepo HearseRepo;

        //Constructor
        public CalendarEntryRepo(HearseRepo hearseRepo)
        {
            Eventslist = new List<CalendarEntry>();
            HearseRepo = hearseRepo;
        }


        // Simple method for adding an Event to the Event list.
        public void AddEvent(CalendarEntry Event)
        {
            Eventslist.Add(Event);
        }


        // The CreateEvent method takes a number of parameters which we will use to make an event in our calendar.
        public bool CreateEvent(DateTime start, DateTime end, string address, string comment, bool hearseNeeded)
        {

            // Here we run an if-else loop on whether or not a hearse is needed for the event.
            if (hearseNeeded)
            {
                bool available = true;

                // It will then run through the Event list to see if a hearse is available by checking if the start is later than the end or the end is before the start.
                foreach (Hearse i in HearseRepo.GetCopyHearses())
                {
                    foreach (CalendarEntry _Event in Eventslist)
                    {

                        if (!(_Event.Status == Status.Deleted) && _Event.Hearse == i && !(_Event.End < start || _Event.Start > end))
                        {
                            available = false;

                        }
                    }

                    // If it is neither of those, it means the hearse is available and the AddEvent method is called to add the newly made event to the list.
                    if (available)
                    {
                        CalendarEntry Event = new CalendarEntry(FindHighestKey() + 1, start, end, address, comment, Status.NewlyMade, i);
                        AddEvent(Event);
                        return true;
                    }
                    else
                    {
                        available = true;
                    }
                }
                return false;
            }

            // If a hearse is not needed. It will simply call the method AddEvent to add the event to the list.
            else
            {
                CalendarEntry _Event = new CalendarEntry(FindHighestKey() + 1, start, end, address, comment, Status.NewlyMade, null);
                AddEvent(_Event);
                return true;
            }

        }


        // This method is for altering an already existing event in the list. It uses the same parameters as the CreateEvent method above.
        public bool AlterEvent(int key, int hearse, string start, string end, string address, string comment)
        {

            // We instansiate a new _Event object of the CalendarEntry class followed by the data which the object shall contain.
            CalendarEntry _Event = new CalendarEntry(0, DateTime.Now, DateTime.Now, "", "", Status.Deleted, null);

            // We run a foreach loop, checking every object(i) of the CalendarEntry type in the list of events.
            foreach (CalendarEntry i in Eventslist)
            {
                // If loop to check if the item's key is equal to the key given in the method parameter, then that event referred to in the AlterEvent method is set to be equal to the _Event object.
                if (i.Key == key)
                {
                    _Event = i;
                }
            }

            // If the key given in the AlterEvent method is equal to 0, then that key is not found.
            if (_Event.Key == 0)
            {
                throw new IndexOutOfRangeException("Identifikationsnøglen blev ikke fundet.");
            }

            // If the hearse is equal to 0, set its value to null.
            if (hearse == 0)
            {
                _Event.Hearse = null;
            }

            // If it is not 0, call the GetHearse method to assign a hearse to the event.
            else
            {
                _Event.Hearse = HearseRepo.GetHearse(hearse);
            }

            // if the start value is not equal to 0, continue the code block.
            if (!(start == null))
            {

                // If loop to attempt to parse the value of the start parameter to the ostart variable.
                if (DateTime.TryParse(start, out DateTime ostart))
                {
                    // If the parse succeeded; set the boolean, Available, to true.
                    bool Available = true;

                    // If not, simply set the Start value to the ostart variable.
                    if (Available)
                    {
                        _Event.Start = ostart;
                    }
                    else
                    {
                        Available = true;
                    }


                }

                // If the Event.Start is not equal to the ostart variable, return false.
                if (!(_Event.Start == ostart))
                {
                    return false;
                }
            }

            // If the end is not equal to null, continue the code block.
            if (!(end == null))
            {

                // Try to parse the end value to the oend variable.
                if (DateTime.TryParse(end, out DateTime oend))
                {
                    // If it succeeded; set the boolean, Available, to true.
                    bool available = true;

                    if (available)
                    {
                        _Event.Start = oend;
                    }
                    else
                    {
                        available = true;
                    }
                }
                if (_Event.End == oend)
                {
                    return true;
                }
            }

            // If the address is not equal to null; set the address as the Event.Address.
            if (!(address == null))
            {
                _Event.Address = address;
            }

            // If the comment is not equal to null; set the comment as the Event.Comment.
            if (!(comment == null))
            {
                _Event.Comment = comment;
            }

            // If the _Event.Status is not equal to status.NewlyMade; set the _Event.Status as the Status.Changed.
            if (!(_Event.Status == Status.NewlyMade))
            {
                _Event.Status = Status.Changed;

            }
            foreach (CalendarEntry i in Eventslist)
            {
                // If loop to check if the item's key is equal to the key given in the method parameter, then that event referred to in the AlterEvent method is set to be equal to the _Event object.
                if (i.Key == key)
                {
                    i.Status = _Event.Status;
                    i.Hearse = _Event.Hearse;
                    i.Address = _Event.Address;
                    i.Comment = _Event.Comment;
                    i.End = _Event.End;
                    i.Start = _Event.Start;
                }
            }
            return true;

        }


        // 
        public void StartUpEvent(int key, DateTime start, DateTime end, string address, string comment, Hearse hearse = null)
        {
            CalendarEntry Event = new CalendarEntry(key, start, end, address, comment, Status.UnChanged, hearse);

            AddEvent(Event);
        }


        // Method for finding the highest key (perhaps for finding the maximum number of objects in the Event list?)
        private int FindHighestKey()
        {
            // int variable made and set to 0.
            int highest = 0;

            // foreach loop to check every object in the list...
            foreach (CalendarEntry i in Eventslist)
            {
                if (i.Key > highest)
                {
                    highest = i.Key;
                }
            }
            return highest;
        }


        // Method for making a temperary  list of CalendarEntry.
        public List<CalendarEntry> GetCopyEvents()
        {
            List<CalendarEntry> tempLists = Eventslist.ToList();
            return tempLists;
        }


        // Method for deleting an event, by running through the list until it find the object, with the same key as the one provided in the parameter.
        public bool DeleteEvent(int key)
        {
            foreach (CalendarEntry i in Eventslist)
            {
                if (i.Key == key)
                {
                    i.Status = Status.Deleted;
                    return true;
                }
            }
            return false;
        }
    }
}
