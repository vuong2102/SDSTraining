﻿using GrpcService1.Models.Entity;
using GrpcService1.Repository.Interface;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace GrpcService1.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IStatelessSession statelessSession;
        private readonly ISession session;
        private static readonly StudentRepository instance = new StudentRepository();
        public StudentRepository()
        {
            statelessSession = NHibernateConfig.OpenStatelessSession();
            session = NHibernateConfig.OpenSession();
        }
        public static StudentRepository GetInstance()
        {
            return instance;
        }
        public List<Student> GetListStudents()
        {
            List<Student> students = new List<Student>();
            return session.Query<Student>()
                    .Fetch(s => s.Class)
                    .ToList();
        }
        public int GenerateID()
        {
            int maxID = statelessSession.Query<Student>()
                     .Max(s => s.ID);
            return ++maxID;
        }
        public Student FindStudentById(int id)
        {
            var student = statelessSession.Query<Student>()
                    .Fetch(s => s.Class)
                    .Where(s => s.ID == id)
                    .FirstOrDefault();
            return student;
        }
        public Boolean RemoveStudent(Student student)
        {
            using (var transaction = statelessSession.BeginTransaction())
            {
                try
                {
                    // Lấy sinh viên cần xóa từ cơ sở dữ liệu
                    var studentToRemove = statelessSession.Get<Student>(student.ID);
                    if (studentToRemove != null)
                    {
                        // Xóa sinh viên khỏi cơ sở dữ liệu
                        statelessSession.Delete(studentToRemove);

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
        //Chi ket hop dc khi dung Session
        public Boolean SaveOrUpdateStudent(Student student )
        {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        if(student.ID==0) student.ID = GenerateID();
                        session.SaveOrUpdate(student);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        transaction.Rollback();
                    }
                }
            
            return false;
        }
        public Boolean InsertStudent(Student student)
        {
                using (var transaction = statelessSession.BeginTransaction())
                {
                    try
                    {
                        student.ID = GenerateID();
                        statelessSession.Insert(student);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        transaction.Rollback();
                    }
                }
            
            return false;
        }
        public Boolean UpdateStudent(Student student)
        {
            using (var transaction = statelessSession.BeginTransaction())
            {
                try
                {
                    statelessSession.Update(student);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    transaction.Rollback();
                }
            }
            return false;
        }
        public List<Student> SortData()
        {
                return session.Query<Student>()
                    .Fetch(s => s.Class)
                    .OrderBy(x => x.Name)
                    .ThenByDescending(x => x.DateOfBirth)
                    .ToList();
            
        }

        public async Task<PageView<Student>> GetDataPageAsync(int pageNumber, int pageSize, StudentFilter studentFilter)
        {
                var query = statelessSession.Query<Student>(); // Implement NHibernate query here
                query = Filter(query, studentFilter);
                var pagedData = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                var total = query.Count();
                return new PageView<Student>
                {
                    Data = pagedData,
                    PageCount = total // Đây là tổng số mục
                };
        }
        private IQueryable<Student>? Filter(IQueryable<Student> query, StudentFilter studentFilter)
        {
            if (studentFilter.Name != null && !studentFilter.Name.Equals(""))
            {
                query = query.Where(c => c.Name.Contains(studentFilter.Name));
            }
            if (studentFilter.Address != null && !studentFilter.Address.Equals(""))
            {
                query = query.Where(c => c.Address.Contains(studentFilter.Address));
            }
            if (studentFilter.StartDate != null)
            {
                query = query.Where(student => student.DateOfBirth >= studentFilter.StartDate);
            }
            if (studentFilter.EndDate != null)
            {
                query = query.Where(student => student.DateOfBirth <= studentFilter.EndDate);
            }
            if (studentFilter.ClassId != -1)
            {
                query = query.Where(student => student.Class.ID == studentFilter.ClassId);
            }
            if (studentFilter.Id != -1)
            {
                query = query.Where(student => student.ID == studentFilter.Id);
            }
            return query;
        }
    }
}
