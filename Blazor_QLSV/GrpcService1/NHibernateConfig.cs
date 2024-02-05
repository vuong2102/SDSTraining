using GrpcService1.Models.Dto;
using GrpcService1.Models.Mapping;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;
using NHibernate.Dialect;
using NHibernate.Driver;
namespace GrpcService1
{
    public class NHibernateConfig
    {
        public static ISessionFactory BuildSessionFactory()
        {
            var cfg = new Configuration();

            string connectionString = "Data Source=LAPTOP-CUA_VUON\\SQLEXPRESS;Initial Catalog=qlsv2;User ID=sa;Password=21022002";
            cfg.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
            });
            var mapper = new ModelMapper();
            mapper.AddMapping(typeof(StudentMapping));
            mapper.AddMapping(typeof(TeacherMapping));
            mapper.AddMapping(typeof(ClassMapping));
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(domainMapping);
            return cfg.BuildSessionFactory();
        }
    }
}
