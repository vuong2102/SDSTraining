using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Security.Claims;

namespace QLSV.Models.Mappings
{
    class RoomClasssMapping : ClassMapping<RoomClass>
    {
        public RoomClasssMapping()
        {
            Table("RoomClass");
            Id(x => x.ID, m => m.Column("ID"));
            Property(x => x.Name);
            Property(x => x.Subject);
            ManyToOne(x => x.Teacher, m => m.Column("Teacher"));
            Bag(x => x.Students, c =>
            {
                c.Key(k => k.Column("Class"));
                c.Cascade(Cascade.All
                          | Cascade.DeleteOrphans);
            }, r => r.OneToMany());

        }

    }
}