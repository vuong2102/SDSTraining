using GrpcService1.Models.Entity;
using GrpcService1.Repository.Interface;
using NHibernate;
using NHibernate.Linq;

namespace GrpcService1.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly ISessionFactory _session;

        public ClassRepository(ISessionFactory session)
        {
            _session = session;
        }
        public List<Class> GetListClass()
        {
            List<Student> students = new List<Student>();
            using (var session = _session.OpenSession())
            {
                return session.Query<Class>()
                    .Fetch(s => s.Teacher)
                    .ToList();
            }
        }
        public Class GetClassById(int id)
        {
            using (var session = _session.OpenSession())
            {
                return session.Query<Class>()
                    .Where(c => c.ID == id)
                    .First();
            }
        }
    }
}
