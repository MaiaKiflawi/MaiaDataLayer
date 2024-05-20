using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Event;

namespace ViewModel
{
    public class UsersDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Users();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Users user = entity as Users;
            user.Id = int.Parse(reader["ID"].ToString());
            user.UserName = reader["userName"].ToString();
            user.FirstName = reader["firstName"].ToString();
            user.LastName = reader["lastName"].ToString();
            user.BDate = DateTime.Parse(reader["bDate"].ToString());
            user.Gender = bool.Parse(reader["gender"].ToString());
            user.Phone = reader["phone"].ToString();
            user.Email = reader["email"].ToString();
            user.IsManager = bool.Parse(reader["isManager"].ToString());
            user.Password = reader["password"].ToString();
            user.IsGroupAdmin = bool.Parse(reader["isGroupAdmin"].ToString());
            CityDB cityDB = new CityDB();
            int cityId = int.Parse(reader["cityName"].ToString());
            user.CityName = cityDB.SelectById(cityId);
            return user;
        }

        public UsersList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblUsers";
            UsersList list = new UsersList(ExecuteCommand());
            return list;
        }
        public Users SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblUsers WHERE id=" + id;
            UsersList list = new UsersList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }

        public UsersList SelectByGroup(Groups group)
        {
            command.CommandText = "SELECT tblUsers.* " +
                "FROM (tblUsersGroups INNER JOIN tblUsers ON tblUsersGroups.UserID = tblUsers.ID) " +
                $"WHERE (tblUsersGroups.GroupID = {group.Id})";
            UsersList list = new UsersList(ExecuteCommand());
            return list;
        }

        public UsersList SelectByEvent(Event events)
        {
            command.CommandText = "SELECT tblUsers.* " +
                "FROM (tblUsersEvents INNER JOIN tblUsers ON tblUsersEvents.UserID = tblUsers.ID) " +
                $"WHERE (tblUsersEvents.EventID = {events.Id})";
            UsersList list = new UsersList(ExecuteCommand());
            return list;
        }

        protected override void LoadParameters(BaseEntity entity)
        {
            Users user = entity as Users;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@userName", user.UserName);
            command.Parameters.AddWithValue("@firstName", user.FirstName);
            command.Parameters.AddWithValue("@lastNAme", user.LastName);
            command.Parameters.AddWithValue("@bDate", user.BDate);
            command.Parameters.AddWithValue("@gender", user.Gender);
            command.Parameters.AddWithValue("@phone", user.Phone);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@isManager", user.IsManager);
            if(user.CityName!=null)
                 command.Parameters.AddWithValue("@cityName", user.CityName.Id);
            command.Parameters.AddWithValue("@isGroupAdmin", user.IsGroupAdmin);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@ID", user.Id);
        }

        public int Insert(Users user)
        {
            command.CommandText = "INSERT INTO tblUsers " +
                "(userName, firstName, lastName, bDate, gender, phone, email, isManager, cityName, isGroupAdmin, [password]) " +
                "VALUES (@userName,@firstName,@lastName,@bDate,@gender,@phone,@email," +
                "@isManager,@cityName,@isGroupAdmin,@password)";
            LoadParameters(user);
            return ExecuteCRUD();
        }
        public int Update(Users user)
        {
            command.CommandText = "UPDATE tblUsers SET " +
                "userName = @userName, firstName = @firstName, lastName = @lastName, bDate = @bDate, " +
                "gender = @gender, phone = @phone, email = @email, isManager = @isManager, cityName = @cityName, " +
                "isGroupAdmin = @isGroupAdmin, [password] = @password " +
                "WHERE ID = @ID";
            LoadParameters(user);
            return ExecuteCRUD();
        }
        public int Delete(Users user)
        {
            command.CommandText = $"DELETE FROM tblUsers WHERE ID = {user.Id}";
            return ExecuteCRUD();
        }

        public Users LogIn(Users user)
        {
            command.CommandText = $"SELECT * FROM tblUsers WHERE userName ='{user.UserName}' AND [password] = '{user.Password}'";
            UsersList list = new UsersList(base.ExecuteCommand());
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public UsersList CheckUserName(string userName)
        {
            command.CommandText = $"SELECT * FROM tblUsers WHERE (userName = '{userName}')";
            UsersList list = new UsersList(base.ExecuteCommand());
            return list;
        }

        public UsersList CheckUserNameID(Users users)
        {
            string userName = users.UserName;
            command.CommandText = $"SELECT * FROM tblUsers WHERE (userName = '{userName}')";
            UsersList list = new UsersList(base.ExecuteCommand());
            list.RemoveAll(user => user.Id == users.Id);
            return list;
        }

        public int InsertUserToUGtbl(Users user, Groups group)
        {
            command.CommandText = $"INSERT INTO tblUsersGroups (UserID, GroupID) VALUES ({user.Id},{group.Id})";
            return ExecuteCRUD();
        }

        public int DeleteUserFromUGtbl(Users user, Groups group)
        {
            command.CommandText = $"DELETE FROM tblUsersGroups WHERE (tblUsersGroups.UserID = {user.Id}) AND (tblUsersGroups.GroupID = {group.Id})";
            return ExecuteCRUD();
        }

        public int InsertUserToUEtbl(Users user, Event events)
        {
            command.CommandText = $"INSERT INTO tblUsersEvents (UserID, EventID) VALUES ({user.Id},{events.Id})";
            return ExecuteCRUD();
        }

        public int DeleteUserFromUEtbl(Users user, Event events)
        {
            command.CommandText = $"DELETE FROM tblUsersEvents WHERE (tblUsersEvents.UserID = {user.Id}) AND (tblUsersEvents.EventID = {events.Id})";
            return ExecuteCRUD();
        }
    }
}
