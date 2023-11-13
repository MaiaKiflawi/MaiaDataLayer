using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Event;

namespace ViewModel
{
    internal class EventDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Event();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Event events = entity as Event;
            events.Id = int.Parse(reader["ID"].ToString());
            events.EventEnd = DateTime.Parse(reader["start"].ToString());
            events.EventStart = DateTime.Parse(reader["end"].ToString());
            events.EventNAme = reader["eventName"].ToString();
            GroupsDB groupDB = new GroupsDB();
            int groupId = int.Parse(reader["groupName"].ToString());
            events.GroupOfEvent = groupDB.SelectById(groupId);
            return events;
        }

        public EventList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblEvent";
            EventList list = new EventList(ExecuteCommand());
            return list;
        }

        public Event SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblEvent WHERE id=" + id;
            EventList list = new EventList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }

        public EventList SelectByUser(Users user)
        {
            command.CommandText = "SELECT tblEvent.* " +
                "FROM (tblUsersEvents INNER JOIN tblEvent ON tblUsersEvents.EventID = tblEvent.ID) " +
                $"WHERE (tblUsersEvents.UserID = {user.Id})";
            EventList list = new EventList(ExecuteCommand());
            return list;
        }
    }
}
