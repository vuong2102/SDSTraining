using QLSV.Models;

namespace QLSV.Repository.Interface
{
    interface IStudentRepository
    {
        public List<Student> GetListStudents();
        public void AddNewStudent(Student student);
        public Student FindStudentById(int id);
        public Boolean RemoveStudent(Student student);
        public void UpdateStudentInfor(Student student);
        public List<Student> SortData();
    }
}