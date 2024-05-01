using Model;
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
            group.Id =int.Parse( reader["ID"].ToString());
            group.GroupName = reader["groupName"].ToString();
            group.GroupDescription = reader["groupDescription"].ToString();
            UsersDB usersDB = new UsersDB();
            group.GroupAdmin = usersDB.SelectById(int.Parse(reader["groupAdmin"].ToString()));           
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
            command.CommandText = "SELECT * FROM tblGroups WHERE id=" + id;
            GroupsList list = new GroupsList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }
        public Groups IsGroupName(string name)
        {
            command.CommandText = $"SELECT * FROM tblGroups WHERE groupName='{name}'";
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
            command.Parameters.AddWithValue("@groupName", group.GroupName);
            command.Parameters.AddWithValue("@groupDescription", group.GroupDescription);
            command.Parameters.AddWithValue("@groupAdmin", group.GroupAdmin.Id);

            command.Parameters.AddWithValue("@ID", group.Id);
        }

        public int Insert(Groups group)
        {
            command.CommandText = "INSERT INTO tblGroups (groupName, groupDescription, groupAdmin)" +
                " VALUES (@groupName, @groupDescription, @groupAdmin)";
            LoadParameters(group);
            return ExecuteCRUD();
        }
        public int Update(Groups group)
        {
            command.CommandText = "UPDATE tblGroups SET groupName = @groupName, groupDescription = @groupDescription," +
                "groupAdmin = @groupAdmin WHERE ID = @ID";
            LoadParameters(group);
            return ExecuteCRUD();
        }
        public int Delete(Groups group)
        {
            command.CommandText = $"DELETE FROM tblGroups WHERE ID = {group.Id}";
            return ExecuteCRUD();
        }
    }
}
