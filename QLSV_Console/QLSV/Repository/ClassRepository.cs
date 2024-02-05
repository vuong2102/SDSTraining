using NHibernate;
using NHibernate.Linq;
using QLSV.Models;
using QLSV.Repository.Interface;

namespace QLSV.Repository
{
    internal class ClassRepository : IClassRepository
    {
        private readonly ISessionFactory _session;

        public ClassRepository(ISessionFactory session)
        {
            _session = session;
        }
        public List<RoomClass> GetListClass()
        {
            List<Student> students = new List<Student>();
            using (var session = _session.OpenSession())
            {
                return session.Query<RoomClass>()
                    .Fetch(s => s.Teacher)
                    .ToList();
            }
        }
    }
}