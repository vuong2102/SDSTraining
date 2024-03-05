using GrpcService1.Models.Dto;
using GrpcService1.Models.Mapping;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate;
using NHibernate.Cfg;
using Configuration = NHibernate.Cfg.Configuration;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Configuration;
using NHibernate.SqlCommand;
using FluentNHibernate.Utils;
using log4net;
using NHibernate.Type;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using static GrpcService1.Repository.StudentRepository;
namespace GrpcService1
{
    public class NHibernateConfig
    {
        private static ISessionFactory _sessionFactory;
        public static ISessionFactory BuildSessionFactory()
        {
            var cfg = new Configuration();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            //in ra cau lenh SQL trong console
            BasicConfigurator.Configure();
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
            _sessionFactory = cfg.BuildSessionFactory();
            return _sessionFactory;
        }
        public static IStatelessSession OpenStatelessSession()
        {
            return _sessionFactory.OpenStatelessSession();
        }
        public static NHibernate.ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
