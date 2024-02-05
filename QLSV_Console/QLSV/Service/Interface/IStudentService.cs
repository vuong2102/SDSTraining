using QLSV.Models;

namespace QLSV.Manager.Interface
{
    interface IStudentService
    {
        int GenerateID();
        void ShowListStudents(List<Student> students);
        void GetListStudents();
        Boolean CheckDate(String date);
        void AddNewStudent();
        void SelectClass(Student student);
        Student FindStudentById(int id);
        void ShowInfoStudent(Student student);
        void EditInfoStudent();
        void ShowStudentById();
        void DeleteStudent();
        void SortByName();
    }
}
