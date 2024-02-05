using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Models
{
    public class Teacher{
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual ICollection<RoomClass> RoomClasses { get; set; }
        public Teacher(int ID, string Name, DateTime DateOfBirth)
        {
            this.ID = ID;
            this.Name = Name;
            this.DateOfBirth = DateOfBirth;
        }
        public Teacher()
        {

        }

    }
}
