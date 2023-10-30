using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CityDB cityDB = new CityDB();
            int cityId = int.Parse(reader["cityName"].ToString());
            user.CityName = cityDB.SelectById(cityId);
            return user;
        }

        public UsersList SelectAll()
        {
            command.CommandText = "SELECT * FROM tbCity";
            UsersList list = new UsersList(ExecuteCommand());
            return list;
        }
        public Users SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tbCity WHERE id=" + id;
            UsersList list = new UsersList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}
