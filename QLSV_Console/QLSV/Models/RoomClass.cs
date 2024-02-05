using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Models
{
    public class RoomClass
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public RoomClass()
        {
            
        }
        public RoomClass(int ID, string Name, string Subject, Teacher teacher)
        {
            this.ID = ID;
            this.Name = Name;
            this.Subject = Subject;
            Teacher = teacher;
        }
    }
}
