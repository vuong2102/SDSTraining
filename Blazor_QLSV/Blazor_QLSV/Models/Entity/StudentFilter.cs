namespace Blazor_QLSV.Models.Entity
{
    public class StudentFilter
    {
        public String Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public String Address { get; set; }
        public int ClassId { get; set; } = -1;
        public int ID { get; set; } = -1;

        public int Months { get; set; }

        public StudentFilter()
        {

        }
    }
}
