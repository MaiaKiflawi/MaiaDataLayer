using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;

namespace ServiceModel
{
    public class CalanderService : ICalanderService
    {
        public int DeleteCity(City city)
        {
            CityDB db = new CityDB();
            return db.Delete(city);
        }

        public int DeleteEvent(Event events)
        {
            EventDB db = new EventDB();
            return db.Delete(events);
        }

        public int DeleteGroup(Groups group)
        {
            UsersList users;
            EventList events = GetEventsByGroup(group);
            //deletes all users who are supposed to attend all the groups events from tblUsersEvents in this group
            foreach (Event item in events)
            {
                users = GetUsersByEvent(item);
                foreach (Users user in users)
                {
                    DeleteUserFromEvent(user,item);
                }
                DeleteEvent(item);
            }
            users = GetUsersByGroup(group);
            //deletes all users who are registered to group from tblUsersGroups in this group
            foreach (Users user in users)
            {
                DeleteUserFromGroup(user, group);
            }
            GroupsDB db = new GroupsDB();
            return db.Delete(group);
        }

        public int DeleteUser(Users user)
        {
            Users manager = GetAllUsers().Find(u => u.IsManager); //finds app manager
            EventList events=GetEventsByUser(user);
            //deletes this user from all the events he is supposed to attend
            //deletes this user from tblUserEvents
            foreach (Event item in events)
                DeleteUserFromEvent(user,item);
            GroupsList groups=GetGroupsByUser(user);
            //deletes this user from all the groups he is registered to 
            //deletes this user from tblUserGroups
            foreach (Groups group in groups)
                DeleteUserFromGroup(user, group);
            groups=new GroupsList(GetAllGroups().FindAll(g=>g.GroupAdmin.Id==user.Id).ToList()); 
            //if this user was a group admin - this will make the app manager the new group admin for his groups
            foreach (Groups group in groups)
            {
                group.GroupAdmin.Id = manager.Id;
                UpdateGroup(group);
            }
            UsersDB db = new UsersDB();
            return db.Delete(user);
        }

        public CityList GetAllCities()
        {
            CityDB db = new CityDB();
            CityList list = db.SelectAll();
            return list;
        }
       
        public EventList GetAllEvents()
        {
            EventDB db = new EventDB();
            EventList list = db.SelectAll();
            return list;
        }
        public GroupsList GetAllGroups()
        {
            GroupsDB db = new GroupsDB();
            GroupsList list = db.SelectAll();
            return list;
        }
        public UsersList GetAllUsers()
        {
            UsersDB db = new UsersDB();
            UsersList list = db.SelectAll();
            return list;
        }
        public EventList GetEventsByUser(Users user)
        {
            EventDB db = new EventDB();
            EventList list = db.SelectByUser(user);
            return list;
        }
        public EventList GetEventsByGroup(Groups group)
        {
            EventDB db = new EventDB();
            EventList list = db.SelectByGroup(group);
            return list;
        }
        public GroupsList GetGroupsByUser(Users user)
        {
            GroupsDB db = new GroupsDB();
            GroupsList list = db.SelectByUser(user);
            return list;
        }
        public UsersList GetUsersByEvent(Event events)
        {
            UsersDB db = new UsersDB();
            UsersList list = db.SelectByEvent(events);
            return list;
        }
        public UsersList GetUsersByGroup(Groups group)
        {
            UsersDB db = new UsersDB();
            UsersList list = db.SelectByGroup(group);
            return list;
        }

        public int InsertCity(City city)
        {
            CityDB db = new CityDB();
            return db.Insert(city);
        }

        public int InsertEvent(Event events)
        {
            EventDB db = new EventDB();
            return db.Insert(events);
        }

        public int InsertGroup(Groups group)
        {
            GroupsDB db = new GroupsDB();
            return db.Insert(group);
        }

        public int InsertUser(Users user)
        {
            UsersDB db = new UsersDB();
            return db.Insert(user);
        }

        public Users Login(Users user)
        {
            UsersDB db = new UsersDB();
            return db.LogIn(user);
        }

        public int UpdateCity(City city)
        {
            CityDB db = new CityDB();
            return db.Update(city);
        }

        public int UpdateEvent(Event events)
        {
            EventDB db = new EventDB();
            return db.Update(events);
        }

        public int UpdateGroup(Groups group)
        {
            GroupsDB db = new GroupsDB();
            return db.Update(group);
        }

        public int UpdateUser(Users user)
        {
            UsersDB db = new UsersDB();
            return db.Update(user);
        }

        public bool IsUsernameFree(string username)
        {
            UsersDB db = new UsersDB();
            UsersList list = db.CheckUserName(username);
            return list.Count == 0;
        }

        public bool IsGroupNameFree(string groupName)
        {
            GroupsDB db = new GroupsDB();
            Groups group = db.IsGroupName(groupName);
            return group==null;
        }

        public int InsertUserToGroup(Users user, Groups group)
        {
            UsersDB db = new UsersDB();
            return db.InsertUserToUGtbl(user, group);
        }

        public int DeleteUserFromGroup(Users user, Groups group)
        {
            UsersDB db = new UsersDB();
            return db.DeleteUserFromUGtbl(user, group);
        }

        public int InsertUserToEvent(Users user, Event events)
        {
            UsersDB dB = new UsersDB();
            return dB.InsertUserToUEtbl(user, events);
        }

        public int DeleteUserFromEvent(Users user, Event events)
        {
            UsersDB dB = new UsersDB();
            return dB.DeleteUserFromUEtbl(user, events);
        }
    }
}
