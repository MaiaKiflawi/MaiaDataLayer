using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class City: BaseEntity
    {
        protected string cityName;
        protected Country countryName;
        public Country CountryName
        {
            get { return this.countryName; }
            set { this.countryName = value; }
        }
        public string CityName
        {
            get { return this.cityName; }
            set { this.cityName = value; }
        }
    }

    public class CityList : List<City>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public CityList() { }
        //המרה אוסף גנרי לרשימת ערים
        public CityList(IEnumerable<City> list): base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת ערים
        public CityList(IEnumerable<BaseEntity> list)
            : base(list.Cast<City>().ToList()) { }
    }
}
