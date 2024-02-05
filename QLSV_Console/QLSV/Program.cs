using QLSV.Manager.Interface;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using QLSV.Repository.Interface;
using QLSV.Repository;

namespace QLSV.Manager
{
    public class Program
    {
        static void Main(string[] arg)
        {
            var serviceProvider = new ServiceCollection()
            .BuildSessionFactory()
            .AddSingleton<IStudentRepository, StudentRepository>()
            .AddSingleton<IStudentService, StudentService>()
            .AddSingleton<IClassRepository, ClassRepository>()
            .AddSingleton<IMainService, Main>()
            .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                //Main Program
                var iMain = serviceProvider.GetService<IMainService>();
                while (true)
                {
                    menu();
                    Console.Write("Nhap tuy chon: ");
                    int key = Convert.ToInt32(Console.ReadLine());
                    switch (key)
                    {
                        case 1:
                            //_iStudentRepo.GetListStudents(); break;
                            iMain.GetListStudent(); break;
                        case 2:
                            iMain.AddNewStudent(); break;
                        case 3:
                            iMain.EditStudent(); break;
                        case 4:
                            iMain.DeleteStudent(); break;
                        case 5:
                            iMain.FindStudentByID(); break;
                        case 6:
                            iMain.SortStudentByName(); break;
                        case 0:
                            iMain.ExitConsole(); return;
                        default: 
                            iMain.NoExistFunction(); break;
                    }
                }
            } 
        }
        static void menu()
        {
            Console.WriteLine("\nCHUONG TRINH QUAN LY SINH VIEN");
            Console.WriteLine("*************************MENU**************************");
            Console.WriteLine("**  1. Danh sach sinh vien                           **");
            Console.WriteLine("**  2. Them moi sinh vien                            **");
            Console.WriteLine("**  3. Chinh sua thong tin sinh vien                 **");
            Console.WriteLine("**  4. Xoa sinh vien                                 **");
            Console.WriteLine("**  5. Tim kiem sinh vien theo ma so sinh vien.      **");
            Console.WriteLine("**  6. Sap xep sinh vien theo ten                    **");
            Console.WriteLine("**  0. Thoat                                         **");
            Console.WriteLine("*******************************************************");
        }
    }
}
