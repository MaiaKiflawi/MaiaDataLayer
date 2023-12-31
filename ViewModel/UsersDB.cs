﻿using Model;
using System;
using System.Collections.Generic;
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
            command.Parameters.AddWithValue("@ID", user.Id);
            command.Parameters.AddWithValue("@userName", user.UserName);
            command.Parameters.AddWithValue("@firstName", user.FirstName);
            command.Parameters.AddWithValue("@lastNAme", user.LastName);
            command.Parameters.AddWithValue("@bDate", user.BDate);
            command.Parameters.AddWithValue("@gender", user.Gender);
            command.Parameters.AddWithValue("@phone", user.Phone);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@isManager", user.IsManager);
            command.Parameters.AddWithValue("@cityName", user.CityName.Id);
            command.Parameters.AddWithValue("@isGroupAdmin", user.IsGroupAdmin);
            command.Parameters.AddWithValue("@password", user.Password);
        }

        public int Insert(Users user)
        {
            command.CommandText = "INSERT INTO tblUsers " +
                "(userName, firstName, lastName, bDate, gender, phone, email, isManager, cityName, isGroupAdmin, password) " +
                "VALUES (@userName,@firstName,@lastName,@bDate,@gender,@phone,@email," +
                "@isManager,@cityName,@isGroupAdmin,@password)";
            LoadParameters(user);
            return ExecuteCRUD();
        }
        public int Update(Users user)
        {
            command.CommandText = "UPDATE tblUsers SET " +
                "userName = @userName, password = @password, firstName = @firstName, lastName = @lastName, " +
                "gender = @gender, phone = @phone, email = @email, cityName = @cityName " +
                "WHERE ID = @ID";
            LoadParameters(user);
            return ExecuteCRUD();
        }
        public int Delete(Users user)
        {
            command.CommandText = "DELETE FROM tblUsers WHERE ID = @ID";
            LoadParameters(user);
            return ExecuteCRUD();
        }

        public Users LogIn(Users user)
        {
            command.CommandText = "SELECT * FROM tblUsers WHERE userName = @userName, password = @password";
            LoadParameters(user);
            UsersList list = new UsersList(base.ExecuteCommand());
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public UsersList CheckUserName(string userName)
        {
            command.CommandText = "SELECT * FROM tblUsers WHERE (userName = @userName)";
            UsersList list = new UsersList(base.ExecuteCommand());
            return list;
        }
    }
}
