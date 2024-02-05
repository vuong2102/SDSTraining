using NHibernate;
using NHibernate.Linq;
using QLSV.Models;
using QLSV.Repository.Interface;

namespace QLSV.Repository
{
    class StudentRepository : IStudentRepository
    {
        private readonly ISessionFactory _session;

        public StudentRepository(ISessionFactory session)
        {
            _session = session;
        }
        public List<Student> GetListStudents()
        {
            List<Student> students = new List<Student>();
            using (var session = _session.OpenSession())
            {
                return session.Query<Student>()
                    .Fetch(s => s.RoomClass)
                    .ToList();
            }
        } 
        public void AddNewStudent(Student student)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(student);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        transaction.Rollback();
                    }
                }
            }
        }
        public Student FindStudentById(int id)
        {
            using (var session = _session.OpenSession())
            {
                var student = session.Query<Student>()
                    .Fetch(s => s.RoomClass)
                    .Where(s => s.ID == id)
                    .FirstOrDefault();
                return student;
            }
        }
        public Boolean RemoveStudent(Student student)
        {
            using (var session = _session.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    // Lấy sinh viên cần xóa từ cơ sở dữ liệu
                    var studentToRemove = session.Get<Student>(student.ID);
                    if (studentToRemove != null)
                    {
                        // Xóa sinh viên khỏi cơ sở dữ liệu
                        session.Delete(studentToRemove);

                        // Commit giao dịch
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Student with ID {student.ID} not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    transaction.Rollback();
                }
                return false;
            }
        }
        public void UpdateStudentInfor(Student student)
        {
            using (var session = _session.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(student);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"An error occurred: {ex.Message}");
                        transaction.Rollback();
                    }
                }
            }
        }
        public List<Student> SortData()
        {
            using (var session = _session.OpenSession())
            {
                return session.Query<Student>()
                    .Fetch(s => s.RoomClass)
                    .OrderBy(x => x.Name)
                    .ThenByDescending(x => x.DateOfBirth)
                    .ToList();
            }
        }
    }
}