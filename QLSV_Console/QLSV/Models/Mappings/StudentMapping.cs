using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using FluentNHibernate.Mapping;
using QLSV.Models;

namespace QLSV.Models.Mappings
{
    class StudentMapping : ClassMapping<Student>
    {
        public StudentMapping() {

            Table("Student");
            Id(p => p.ID, map => map.Column("ID"));
            Property(p => p.Name);
            Property(p => p.DateOfBirth);
            Property(p => p.Address);
            ManyToOne(p => p.RoomClass, m => m.Column("RoomClass"));
        }
    }
}
