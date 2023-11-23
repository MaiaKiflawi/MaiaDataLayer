using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Users: BaseEntity
    {
        protected string userName;
        protected string firstName;
        protected string lastName;
        protected DateTime bDate;
        protected bool gender;
        protected string phone;
        protected City cityName;
        protected string email;
        protected bool isManager;
        protected string password;
        protected bool isGroudAdmin;
        [DataMember]
        public string UserName 
        {
            get { return this.userName; }
            set { this.userName = value; } 
        }
        [DataMember]
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }
        [DataMember]
        public DateTime BDate
        {
            get { return this.bDate; }
            set { this.bDate = value; }
        }
        [DataMember]
        public bool Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }
        [DataMember]
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        [DataMember]
        public City CityName
        {
            get { return this.cityName; }
            set { this.cityName = value; }
        }
        [DataMember]
        public string Email 
        {
            get { return this.email; }
            set { this.email = value; }
        }
        [DataMember]
        public bool IsManager
        {
            get { return this.isManager; }
            set { this.isManager = value; }
        }
        [DataMember]
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        [DataMember]
        public bool IsGroupAdmin 
        {
            get { return isGroudAdmin; }
            set { isGroudAdmin = value; }
        }
    }
    [CollectionDataContract]
    public class UsersList : List<Users>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public UsersList() { }
        //המרה אוסף גנרי לרשימת משתמשים
        public UsersList(IEnumerable<Users> list): base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת משתמשים
        public UsersList(IEnumerable<BaseEntity> list)
            : base(list.Cast<Users>().ToList()) { }
    }
}
