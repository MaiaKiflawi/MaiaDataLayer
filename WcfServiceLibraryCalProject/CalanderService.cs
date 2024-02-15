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
            GroupsDB db = new GroupsDB();
            return db.Delete(group);
        }

        public int DeleteUser(Users user)
        {
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

        public int DeleteUserToGroup(Users user, Groups group)
        {
            UsersDB db = new UsersDB();
            return db.DeleteUserToUGtbl(user, group);
        }
    }
}
