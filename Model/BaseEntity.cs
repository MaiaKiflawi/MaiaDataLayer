using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class BaseEntity
    {
        protected int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; } 
        }
    }
}
