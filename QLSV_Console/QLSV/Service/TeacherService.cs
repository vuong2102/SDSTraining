using QLSV.Manager.Interface;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Manager
{
    internal class TeacherService : ITeacherService
    {

        List<Teacher> teachers = new List<Teacher>();

        public TeacherService()
        {
           this.teachers = GetListTeachers();
        }
        public List<Teacher> GetListTeachers()
        {
            {
                Teacher t1 = new Teacher(1, "Vuong", new DateTime(1999, 2, 21));
                Teacher t2 = new Teacher(2, "Binh", new DateTime(1996, 10, 11));
                Teacher t3 = new Teacher(3, "Tuan", new DateTime(2000, 3, 12));
                Teacher t4 = new Teacher(4, "Linh", new DateTime(2001, 10, 6));
                teachers.Add(t1);
                teachers.Add(t2);
                teachers.Add(t3);
                teachers.Add(t4);
                return teachers;
            }
        }

    }
}
