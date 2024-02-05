using GrpcService1.Models.Entity;
using Share;
namespace GrpcService1.Models.Mapper
{
    public class StudentMapper
    {
        public StudentGrpc ClassToClassGrpc(Student student)
        {
            StudentGrpc studentGrpc = new StudentGrpc();
            studentGrpc.Id = student.ID;
            studentGrpc.Name = student.Name;
            studentGrpc.Address = student.Address;
            studentGrpc.DateOfBirth = student.DateOfBirth;
            if (student.Class == null)
            {
                student.Class = new Class();
            }
            studentGrpc.ClassId = student.Class.ID;
            //Console.WriteLine(studentGrpc.ClassId);
            return studentGrpc;
        }
        public Student ClassGrpcToClass(StudentGrpc studentGrpc)
        {
            Student student = new Student();
            student.ID = studentGrpc.Id;
            student.Name = studentGrpc.Name;
            student.Address = studentGrpc.Address;
            student.DateOfBirth = studentGrpc.DateOfBirth;
            if (student.Class == null)
            {
                student.Class = new Class();
            }
            student.Class.ID = studentGrpc.ClassId;
            return student;
        }
    }
}
