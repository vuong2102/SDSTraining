﻿using GrpcService1.Models.Entity;
using GrpcService1.Models.Mapper;
using GrpcService1.Repository.Interface;
using ProtoBuf.Grpc;
using Share;
using static GrpcService1.Repository.StudentRepository;

namespace GrpcService1.Services
{
    public class StudentService : StudentProto
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IStudentRepository _studentRepository;

        StudentMapper studentMapper = new StudentMapper();

        public StudentService(ILogger<GreeterService> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }
        public BooleanGrpc AddNewStudent(StudentGrpc request, CallContext context = default)
        {
            Student student = studentMapper.ClassGrpcToClass(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _studentRepository.SaveOrUpdateStudent(student);
            return booleanGrpc;
        }
        public BooleanGrpc UpdateStudent(StudentGrpc request, CallContext context = default)
        {
            Student student = studentMapper.ClassGrpcToClass(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _studentRepository.UpdateStudent(student);
            return booleanGrpc;
        }

        public BooleanGrpc DeleteStudent(StudentGrpc request, CallContext context = default)
        {
            Student student = studentMapper.ClassGrpcToClass(request);
            BooleanGrpc booleanGrpc = new BooleanGrpc();
            booleanGrpc.Empty = new Empty();
            booleanGrpc.Result = _studentRepository.RemoveStudent(student);
            return booleanGrpc;
        }

        public ListStudent GetAllStudents(Empty request, CallContext context = default)
        {
            ListStudent listStudent = new ListStudent();
            List<Student> students = _studentRepository.GetListStudents();
            foreach (Student student in students)
            {
                StudentGrpc studentGrpc = studentMapper.ClassToClassGrpc(student);
                listStudent.Students.Add(studentGrpc);
            }
            return listStudent;
        }

        public async Task<PageViewGrpc> GetDataPageAsync(PageViewGrpc pageViewGrpc, CallContext context = default)
        {
            try
            {
                PageView<Student> pageView = new PageView<Student>();
                pageView.PageNumber = pageViewGrpc.PageNumber;
                pageView.PageSize = pageViewGrpc.PageSize;
                StudentFilter studentFilter = new StudentFilter();
                studentFilter = MapGrpcToStudentFilter(pageViewGrpc.StudentFilterGrpc);
                var result = await _studentRepository.GetDataPageAsync(pageView.PageNumber, pageView.PageSize, studentFilter);
                var resultGrpc = new PageViewGrpc();
                resultGrpc.PageCount = result.PageCount;
                resultGrpc.PageNumber = result.PageNumber;
                resultGrpc.PageSize = resultGrpc.PageSize;
                foreach (var item in result.Data)
                {
                    //Console.WriteLine(item.Class.ID);
                    StudentGrpc studentGrpc = studentMapper.ClassToClassGrpc(item);
                    resultGrpc.Students.Add(studentGrpc);
                }
                return resultGrpc;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public StudentGrpc GetStudentById(IntGrpc request, CallContext context = default)
        {
            try
            {
                StudentGrpc studentGrpc = new StudentGrpc();
                Student student = _studentRepository.FindStudentById(request.Id);
                studentGrpc = studentMapper.ClassToClassGrpc(student);
                return studentGrpc;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private StudentFilter MapGrpcToStudentFilter(StudentFilterGrpc studentFilterGrpc)
        {
            var filter = new StudentFilter();
            filter.StartDate = studentFilterGrpc.StartDate;
            filter.EndDate = studentFilterGrpc.EndDate;
            filter.Name = studentFilterGrpc.Name;
            filter.ClassId = studentFilterGrpc.ClassId;
            filter.Address = studentFilterGrpc.Address;
            return filter;
        }
    }
}
