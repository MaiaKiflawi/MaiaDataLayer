using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Country: BaseEntity
    {
        protected string countryName;

        public string CountryName
        {
            get { return this.countryName; }
            set { this.countryName = value; }
        }
    }

    public class CountryList : List<Country>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public CountryList() { }
        //המרה אוסף גנרי לרשימת ערים
        public CountryList(IEnumerable<Country> list) : base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת ערים
        public CountryList(IEnumerable<BaseEntity> list)
            : base(list.Cast<Country>().ToList()) { }
    }
}
