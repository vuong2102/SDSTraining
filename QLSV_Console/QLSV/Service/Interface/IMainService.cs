using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Manager.Interface
{
    internal interface IMainService
    {
        void GetListStudent();
        void AddNewStudent();
        void EditStudent();
        void DeleteStudent();
        void FindStudentByID();
        void SortStudentByName();
        void ExitConsole();
        void NoExistFunction();
        
    }
}
