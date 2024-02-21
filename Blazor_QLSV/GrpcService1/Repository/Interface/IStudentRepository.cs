using GrpcService1.Models.Entity;
using static GrpcService1.Repository.StudentRepository;

namespace GrpcService1.Repository.Interface
{
    public interface IStudentRepository
    {
        public List<Student> GetListStudents();
        public Student FindStudentById(int id);
        public Boolean RemoveStudent(Student student);
        public Boolean SaveOrUpdateStudent(Student student);
        public Boolean InsertStudent(Student student);
        public Boolean UpdateStudent(Student student);
        public List<Student> SortData();
        Task<PageView<Student>> GetDataPageAsync(int pageNumber, int pageSize, StudentFilter studentFilter);
    }
}