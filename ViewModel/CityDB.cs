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
            CountryDB countryDB = new CountryDB();
            int countryId = int.Parse(reader["countryName"].ToString());
            city.CountryName = countryDB.SelectById(countryId);
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
    }
}
