using GrpcService1.Models.Entity;

namespace GrpcService1.Repository.Interface
{
    public interface IStudentRepository
    {
        public List<Student> GetListStudents();
        public Boolean AddNewStudent(Student student);
        public Student FindStudentById(int id);
        public Boolean RemoveStudent(Student student);
        public Boolean UpdateStudentInfor(Student student);
        public List<Student> SortData();
        Task<PageView<Student>> GetDataPageAsync(int pageNumber, int pageSize, StudentFilter studentFilter);
    }
}