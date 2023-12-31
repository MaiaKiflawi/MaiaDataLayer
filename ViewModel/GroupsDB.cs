﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Event;

namespace ViewModel
{
    public class GroupsDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Groups group = entity as Groups;
            group.GroupName = reader["groupName"].ToString();
            group.GroupDescription = reader["groupDescription"].ToString();

            UsersDB usersDB = new UsersDB();
            group.Users = usersDB.SelectByGroup(group);

            return group;
        }

        protected override BaseEntity NewEntity()
        {
            return new Groups();
        }

        public GroupsList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblGroups";
            GroupsList list = new GroupsList(ExecuteCommand());
            return list;
        }
        public Groups SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblCity WHERE id=" + id;
            GroupsList list = new GroupsList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }

        public GroupsList SelectByUser(Users user)
        {
            command.CommandText = "SELECT tblGroups.* " +
                "FROM (tblUsersGroups INNER JOIN tblGroups ON tblUsersGroups.GroupID = tblGroups.ID) " +
                $"WHERE (tblUsersGroups.UserID = {user.Id})";
            GroupsList list = new GroupsList(ExecuteCommand());
            return list;
        }

        protected override void LoadParameters(BaseEntity entity)
        {
            Groups group = entity as Groups;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", group.Id);
            command.Parameters.AddWithValue("@groupName", group.GroupName);
            command.Parameters.AddWithValue("@groupDescription", group.GroupDescription);
        }

        public int Insert(Groups group)
        {
            command.CommandText = "INSERT INTO tblGroups (groupName, groupDescription) VALUES (@groupName, @groupDescription)";
            LoadParameters(group);
            return ExecuteCRUD();
        }
        public int Update(Groups group)
        {
            command.CommandText = "UPDATE tblGroups SET groupName = @groupName, groupDescription = @groupDescription";
            LoadParameters(group);
            return ExecuteCRUD();
        }
        public int Delete(Groups group)
        {
            command.CommandText = "DELETE FROM tblGroups WHERE ID = @ID";
            LoadParameters(group);
            return ExecuteCRUD();
        }
    }
}
