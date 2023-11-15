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

        protected override void LoadParameters(BaseEntity entity)
        {
            Country country = entity as Country;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", country.Id);
            command.Parameters.AddWithValue("@countryName", country.CountryName);
        }

        public int Insert(Country country)
        {
            command.CommandText = "INSERT INTO tblCountry (countryName) VALUES (@countryName)";
            LoadParameters(country);
            return ExecuteCRUD();
        }
        public int Update(Country country)
        {
            command.CommandText = "UPDATE tblCountry SET countryName = @countryName WHERE ID = @ID";
            LoadParameters(country);
            return ExecuteCRUD();
        }
        public int Delete(Country country)
        {
            command.CommandText = "DELETE FROM tblCountry WHERE ID = @ID";
            LoadParameters(country);
            return ExecuteCRUD();
        }
    }
}
