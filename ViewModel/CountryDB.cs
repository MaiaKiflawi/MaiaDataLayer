using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CountryDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Country();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Country country = entity as Country;
            country.Id = int.Parse(reader["ID"].ToString());
            country.CountryName = reader["countryName"].ToString();
            return country;
        }
        public CountryList SelectAll()
        {
            command.CommandText = "SELECT * FROM tblCountry";
            CountryList list = new CountryList(ExecuteCommand());
            return list;
        }
        public Country SelectById(int id)
        {
            command.CommandText = "SELECT * FROM tblCountry WHERE id=" + id;
            CountryList list = new CountryList(ExecuteCommand());
            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}
