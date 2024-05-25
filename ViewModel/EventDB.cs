using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Model.Event;

namespace ViewModel
{
    public class EventDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Event();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Event events = entity as Event;
            events.Id = int.Parse(reader["ID"].ToString());
            events.EventStart = DateTime.Parse(reader["eventStart"].ToString());
            events.EventEnd = DateTime.Parse(reader["eventEnd"].ToString());
            events.EventName = reader["eventName"].ToString();
            GroupsDB groupDB = new GroupsDB();
            int groupId = int.Parse(reader["eventGroup"].ToString());
            events.EventGroup = groupDB.SelectById(groupId);
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
        public EventList SelectByGroup(Groups group)
        {
            command.CommandText = "SELECT * " +
                $"FROM tblEvent WHERE (eventGroup = {group.Id})";
            EventList list = new EventList(ExecuteCommand());
            return list;
        }

        protected override void LoadParameters(BaseEntity entity)
        {
            Event events = entity as Event;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@eventStart", events.EventStart);
            command.Parameters.AddWithValue("@eventEnd", events.EventEnd);
            command.Parameters.AddWithValue("@eventName", events.EventName);
            command.Parameters.AddWithValue("@eventGroup", events.EventGroup.Id);
            command.Parameters.AddWithValue("@ID", events.Id);
        }
        
        public int Insert(Event events)
        {
            command.CommandText = "INSERT INTO tblEvent " +
                "(eventStart, eventEnd, eventName, eventGroup) " +
                "VALUES (@eventStart,@eventEnd,@eventName,@eventGroup)";
            LoadParameters(events);
            return ExecuteCRUD();
        }
        public int Update(Event events)
        {
            command.CommandText = "UPDATE tblEvent SET " +
                "eventStart = @eventStart, eventEnd = @eventEnd, eventName = @eventName" +
                "WHERE ID = @ID";
            LoadParameters(events);
            return ExecuteCRUD();
        }
        public int Delete(Event events)
        {
            command.CommandText = $"DELETE FROM tblEvent WHERE ID = {events.Id}";
            return ExecuteCRUD();
        }
        public EventList CheckEventName(string eventName)
        {
            command.CommandText = $"SELECT * FROM tblEvent WHERE (eventName = '{eventName}')";
            EventList list = new EventList(base.ExecuteCommand());
            return list;
        }
    }
}
