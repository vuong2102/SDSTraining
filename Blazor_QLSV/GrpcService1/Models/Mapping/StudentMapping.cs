using GrpcService1.Models.Entity;
using NHibernate.Mapping.ByCode.Conformist;

namespace GrpcService1.Models.Dto
{
    class StudentMapping : ClassMapping<Student>
    {
        public StudentMapping()
        {

            Table("Student");
            Id(p => p.ID, map => map.Column("ID"));
            Property(p => p.Name);
            Property(p => p.DateOfBirth);
            Property(p => p.Address);
            ManyToOne(p => p.Class, m => m.Column("RoomClass"));
        }
    }
}
