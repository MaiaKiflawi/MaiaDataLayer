using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Event : BaseEntity
    {
        protected DateTime eventStart;
        protected DateTime eventEnd;
        protected string eventName;
        protected Groups eventGroup;
        [DataMember]
        public DateTime EventStart
        {
            get { return eventStart; }
            set { eventStart = value; }
        }
        [DataMember]
        public DateTime EventEnd
        {
            get { return eventEnd; }
            set { eventEnd = value; }
        }
        [DataMember]
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
        [DataMember]
        public Groups EventGroup
        {
            get { return eventGroup; }
            set { eventGroup = value; }
        }
    }
    [CollectionDataContract]
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
