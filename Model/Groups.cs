﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Groups: BaseEntity
    {
        protected string groupName;
        protected string groupDescription;
        protected UsersList users;
        protected Users groupAdmin;
        [DataMember]
        public string GroupName 
        {
            get { return groupName; }
            set { groupName = value; }
        }
        [DataMember]
        public string GroupDescription
        {
            get { return groupDescription; }
            set { groupDescription = value; }
        }
        [DataMember]
        public UsersList Users
        {
            get { return users; }
            set { users = value; }
        }
        [DataMember]
        public Users GroupAdmin
        {
            get { return groupAdmin; }
            set { groupAdmin = value; }
        }

    }
    [CollectionDataContract]
    public class GroupsList : List<Groups>
    {
        //בנאי ברירת מחדל - אוסף ריק
        public GroupsList() { }
        //המרה אוסף גנרי לרשימת קבוצות
        public GroupsList(IEnumerable<Groups> list) : base(list) { }
        //המרה מטה מטיפוס בסיס לרשימת קבוצות
        public GroupsList(IEnumerable<BaseEntity> list)
            : base(list.Cast<Groups>().ToList()) { }
    }
}
