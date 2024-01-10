using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CityDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new City();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            City city = entity as City;
            city.Id = int.Parse(reader["ID"].ToString());
            city.CityName = reader["cityName"].ToString();
            
            return city;
        }

        public CityList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblCity";
            CityList list = new CityList(ExecuteCommand());
            return list;
        }

        public City SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblCity WHERE id=" + id;
            CityList list = new CityList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }

        protected override void LoadParameters(BaseEntity entity)
        {
            City city = entity as City;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", city.Id);
            command.Parameters.AddWithValue("@cityName", city.CityName);
        }

        public int Insert(City city)
        {
            command.CommandText = "INSERT INTO tblCity (cityName,countryName) VALUES (@cityName,@countryName)";
            LoadParameters(city);
            return ExecuteCRUD();
        }
        public int Update(City city)
        {
            command.CommandText = "UPDATE tblCity SET cityName = @cityName, countryName = @countryName WHERE ID = @ID";
            LoadParameters(city);
            return ExecuteCRUD();
        }
        public int Delete(City city)
        {
            command.CommandText = "DELETE FROM tblCity WHERE ID = @ID";
            LoadParameters(city);
            return ExecuteCRUD();
        }
    }
}
