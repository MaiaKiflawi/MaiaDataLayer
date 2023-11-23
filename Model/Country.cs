using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Country: BaseEntity
    {
        protected string countryName;
        [DataMember]
        public string CountryName
        {
            get { return this.countryName; }
            set { this.countryName = value; }
        }
    }
    
    [CollectionDataContract]
    public class CountryList : List<Country>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public CountryList() { }
        //המרה אוסף גנרי לרשימת מדינות
        public CountryList(IEnumerable<Country> list) : base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת מדינות
        public CountryList(IEnumerable<BaseEntity> list)
            : base(list.Cast<Country>().ToList()) { }
    }
}
