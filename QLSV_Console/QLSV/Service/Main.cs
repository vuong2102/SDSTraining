using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QLSV.Manager.Interface;
using QLSV.Repository.Interface;

namespace QLSV.Manager
{
    internal class Main : IMainService
    {
        private readonly IStudentService _studentService;

        public Main(IStudentService _IStudentService)
        {
            _studentService = _IStudentService;
        }

        public void GetListStudent()
        {
            _studentService.GetListStudents();
        }

        public void AddNewStudent()
        {
            Console.WriteLine("\nThem moi sinh vien");
            _studentService.AddNewStudent();
            Console.WriteLine("Them sinh vien thanh cong");
        }

        public void DeleteStudent()
        {
            Console.WriteLine("\nXoa sinh vien");
            _studentService.DeleteStudent();
        }

        public void EditStudent()
        {
            Console.WriteLine("\nChinh sua thong tin sinh vien");
            _studentService.EditInfoStudent();
        }

        public void ExitConsole()
        {
            Console.WriteLine("\nBan da thoat chuong trinh!!!");
        }

        public void FindStudentByID()
        {
            Console.WriteLine("\nTim kiem sinh vien theo ma so sinh vien");
            _studentService.ShowStudentById();
        }
        public void NoExistFunction()
        {
            Console.WriteLine("\nKhong ton tai chuc nang nay!");
        }

        public void SortStudentByName()
        {
            Console.WriteLine("\nSap xep sinh vien theo ten ");
            _studentService.SortByName();
        }
    }
}
