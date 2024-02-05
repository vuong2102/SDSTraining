using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Manager.Interface
{
    internal interface ITeacherService
    {
        List<Teacher> GetListTeachers();
        
    }
}
