using GrpcService1.Models.Entity;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace GrpcService1.Models.Dto
{
    class StudentMapping : ClassMapping<Student>
    {
        public StudentMapping()
        {

            Table("Student");
            SelectBeforeUpdate(true);
            DynamicUpdate(true);
            Id(p => p.ID, map => {
                map.Column("ID");
                map.Generator(Generators.Identity); // Sử dụng generator identity
            });
            Property(p => p.Name);
            Property(p => p.DateOfBirth);
            Property(p => p.Address);
            ManyToOne(p => p.Class, m => m.Column("RoomClass"));
        }
    }
}
