using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
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

        public string UserName 
        {
            get { return this.userName; }
            set { this.userName = value; } 
        }
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }
        public DateTime BDate
        {
            get { return this.bDate; }
            set { this.bDate = value; }
        }
        public bool Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        public City CityName
        {
            get { return this.cityName; }
            set { this.cityName = value; }
        }
        public string Email 
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public bool IsManager
        {
            get { return this.isManager; }
            set { this.isManager = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public bool IsGroupAdmin 
        {
            get { return isGroudAdmin; }
            set { isGroudAdmin = value; }
        }
    }

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
