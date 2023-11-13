using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Event;

namespace ViewModel
{
    internal class GroupsDB : BaseDB
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

    }
}
