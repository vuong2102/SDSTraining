using FluentNHibernate.MappingModel.ClassBased;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using QLSV.Models.Mappings;
using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Configuration = NHibernate.Cfg.Configuration;

namespace QLSV
{
    static class NHibernateConfig
    {
        public static IServiceCollection BuildSessionFactory(this IServiceCollection services)
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
            mapper.AddMapping(typeof(RoomClasssMapping));
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(domainMapping);

            var sessionFactory = cfg.BuildSessionFactory();
            services.AddSingleton<ISessionFactory>(sessionFactory);
            services.AddScoped<ISession>(provider => provider.GetRequiredService<ISessionFactory>().OpenSession());

            return services;
        }
    }
}
