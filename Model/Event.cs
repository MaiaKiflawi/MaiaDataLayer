using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Event: BaseEntity
    {
        protected DateTime eventStart;
        protected DateTime eventEnd;
        protected string eventName;
        protected Groups groupOfEvent;

        public DateTime EventStart 
        {
            get { return eventStart; }
            set { eventStart = value; }
        }

        public DateTime EventEnd
        {
            get { return eventEnd; }
            set { eventEnd = value; }
        }

        public string EventNAme
        {
            get { return eventName; }
            set { eventName = value; }
        }

        public Groups GroupOfEvent
        {
            get { return groupOfEvent; }
            set { groupOfEvent = value; }
        }

        public class EventList : List<Event>
        {
            //בנאי ברירת מחדל - אוסף ריק
            public EventList() { }
            //המרה אוסף גנרי לרשימת משתמשים
            public EventList(IEnumerable<Event> list) : base(list) { }
            //המרה מטה מטיפוס בסיס לרשימת משתמשים
            public EventList(IEnumerable<BaseEntity> list)
                : base(list.Cast<Event>().ToList()) { }
        }
    }
}
