using AntDesign;
using QLSV.Manager.Interface;
using QLSV.Models;
using QLSV.Repository;
using QLSV.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Manager
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        public StudentService(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        //RoomClassService quanliLH = new RoomClassService();
        List<RoomClass> roomClasses = new List<RoomClass>();
        public void GetListStudents()
        {
            List<Student> listStudent = _studentRepository.GetListStudents();
            if (listStudent.Count == 0)
            {
                Console.WriteLine("!!! DANH SACH SINH VIEN TRONG");
            }
            else
            {
                ShowListStudents(listStudent);
            }
        }
        public void ShowListStudents(List<Student> listSV)
        {
            Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, 20} | {4, 15}", "ID", "Name", "Date Of Birth", "Address", "Class");
            if(listSV != null && listSV.Count > 0)
            {
                foreach(Student student in listSV)
                {
                    String year = Convert.ToString(student.DateOfBirth.Year);
                    String month = Convert.ToString(student.DateOfBirth.Month);
                    String day = Convert.ToString(student.DateOfBirth.Day);
                    String sinhNhat = year + "-" + month + "-" + day;
                    Console.WriteLine("{0, -5} | {1, -15} | {2, -15} | {3, 20} | {4, 15}", 
                        student.ID, student.Name, sinhNhat, student.Address, student.RoomClass.Subject);

                }
            }
            //Console.WriteLine();
        }
        public Boolean CheckDate(String date)
        {
            string[] formats = { "yyyy-MM-dd", "yyyy-M-d", "yyyy-M-dd", "yyyy-MM-d"};
            if (DateTime.TryParseExact(date, formats, null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                 return true;
            }
            else
            {
                Console.WriteLine($"Ngày {date} khong hop le, vui long dien dung theo dinh dang YYYY-MM-DD ");
                //CheckDate(date);
            }
            return false;
        }
        public void AddNewStudent()
        {

            Student student = new Student();
            student.ID = Convert.ToInt32(GenerateID());
            Console.Write("Nhap ten sinh vien: ");
            String CheckName = Convert.ToString(Console.ReadLine());
            while(string.IsNullOrWhiteSpace(CheckName))
            {
                Console.Write("!!! Ban chua nhap ten, vui long nhap ten: ");
                //Console.Write("Nhap ten sinh vien: ");
                CheckName = Convert.ToString(Console.ReadLine());
            }
            student.Name = CheckName;


            Console.Write("Nhap ngay sinh cua sinh vien: ");
            String date = Console.ReadLine();
            while (CheckDate(date) != true)
            {
                Console.Write("Nhap ngay sinh cua sinh vien: ");
                date = Console.ReadLine();
            }
            student.DateOfBirth = Convert.ToDateTime(date);
            

            Console.Write("Nhap dia chi sinh vien: ");
            String CheckDiaChi = Convert.ToString(Console.ReadLine());
            while (string.IsNullOrWhiteSpace(CheckDiaChi))
            {
                Console.Write("!!! Ban chua nhap dia chi, vui long nhap dia chi: ");
                CheckDiaChi = Convert.ToString(Console.ReadLine());
            }
            student.Address = CheckDiaChi;
            SelectClass(student);

            _studentRepository.AddNewStudent(student);
        }
        public void SelectClass(Student student)
        {
            List<RoomClass> roomClasses = _classRepository.GetListClass();
            if (roomClasses.Count > 0)
            {
                ShowClasses(roomClasses);
                Console.Write("Chon lop hoc sinh vien: ");
                int key = Convert.ToInt32(Console.ReadLine());
                var roomClass = roomClasses.Where(x => x.ID == key).FirstOrDefault();
                //student.RoomClass = Input.ClassID("Enter class id: ", _classRepository.GetListClass());
                student.RoomClass = roomClass;
                Console.WriteLine(student);
            }
            else Console.WriteLine("CHUA CO LOP HOC NAO!!!");
        }
        public void ShowClasses(List<RoomClass> rooms)
        {
            Console.WriteLine("***************************************************");
            Console.WriteLine("{0, -5} {1, -20} {2, -10} {3, 5}", "ID", "Class", "Subject", "Teacher");
            if (rooms.Count > 0 && rooms != null)
            {
                foreach (RoomClass room in rooms)
                {
                    Console.WriteLine("{0, -5} {1, -20} {2, -10} {3, 5}", room.ID, room.Name, room.Subject, room.Teacher.Name);
                }
            }


        }
        public int GenerateID()
        {
            List<Student> listSV = _studentRepository.GetListStudents();
            int max = 1;
            if (listSV?.Any() == true)
            {
                max = listSV.Max(p => p.ID);
                max++;
            }
            return max;
        }

        public Student FindStudentById(int id)
        {
            List<Student> listSV = _studentRepository.GetListStudents();
            //Student student1 = new Student();
            //?.: Là toán tử kiểm tra null
            //Nếu students null, toàn bộ biểu thức sẽ là null. Nếu students không null, nó tiếp tục gọi phương thức Any().
            //Phương thức Any() là phương thức LINQ, trả về true nếu có ít nhất một phần tử trong tập hợp và false nếu tập hợp là trống.
            if (listSV?.Any() == true)
            {
                var student2 = listSV.Where(student => student.ID == id).FirstOrDefault();
                return student2;

            }
            return null;
        }
        public void ShowInfoStudent(Student student1)
        {
            String year = Convert.ToString(student1.DateOfBirth.Year);
            String month = Convert.ToString(student1.DateOfBirth.Month);
            String day = Convert.ToString(student1.DateOfBirth.Day);
            String sinhNhat = year + "-" + month + "-" + day;
            Console.WriteLine("{0, -5} | {1, -15} | {2, -10} | {3, 20} | {4, 20}", "ID", "Name", "Date Of Birth", "Address", "Class");
            Console.WriteLine("{0, -5} | {1, -15} | {2, -10} | {3, 20} | {4, 20}",
                    student1.ID, student1.Name, sinhNhat, student1.Address, student1.RoomClass.Subject);
        }
        public void EditInfoStudent()
        {
            Console.Write("Nhap ID sinh vien muon chinh sua: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Student student1 = _studentRepository.FindStudentById(id);
            if (student1 != null)
            {
                ShowInfoStudent(student1);

                Console.WriteLine("Thong tin:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Date Of Birth");
                Console.WriteLine("3. Address");
                Console.WriteLine("4. Class");
                Console.WriteLine("0. Return");

                Console.Write("Chon thong tin ban muon sua: ");
                int key = Convert.ToInt32(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        Console.Write("Name: ");
                        student1.Name = Convert.ToString(Console.ReadLine());
                        break;
                    case 2:
                        Console.Write("Date Of Birth: ");
                        student1.DateOfBirth = Convert.ToDateTime(Console.ReadLine());
                        break;
                    case 3:
                        Console.Write("Address: ");
                        student1.Address = Convert.ToString(Console.ReadLine());
                        break;
                    case 4:
                        SelectClass(student1);
                        break;
                    case 0:
                        Console.WriteLine("Thoat chuong trinh");
                        break;
                    default:
                        Console.WriteLine("Ban chua chon cau lenh!!!");
                        break;
                }
                _studentRepository.UpdateStudentInfor(student1);
                Console.WriteLine("Chinh sua thanh cong");
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien");
                EditInfoStudent();
            }
        }

        public void ShowStudentById()
        {
            Console.Write("Nhap Ma Sinh Vien: ");
            int key = Convert.ToInt32(Console.ReadLine());
            Student student1 = _studentRepository.FindStudentById(key);
            if (student1 != null)
            {
                ShowInfoStudent(student1);
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien");
            }
        }

        public void DeleteStudent()
        {
            Console.Write("Nhap Ma Sinh Vien muon xoa: ");
            int key = Convert.ToInt32(Console.ReadLine());
            Student student1 = _studentRepository.FindStudentById(key);
            if (student1 != null)
            {
                var a = _studentRepository.RemoveStudent(student1);
                if (a) Console.WriteLine("Xoa thanh cong");
                else Console.WriteLine("Xoa chua thanh cong");
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien");
            }
        }
        public void SortByName()
        {
            var sortStudent = _studentRepository.SortData();
            ShowListStudents(sortStudent);
        }
    }
}
