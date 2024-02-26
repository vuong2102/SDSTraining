namespace GrpcService1.Models.Entity
{
    public class StudentTest
    {
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Address { get; set; }
        public virtual Class Class { get; set; }

        public StudentTest(string name, DateTime dateOfBirth, string address, Class roomClass)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Address = address;
            Class = roomClass;
        }
        public StudentTest()
        {
        }
        public override string? ToString()
        {
            return $" {Name} - {DateOfBirth.ToString("yyyy/mm/đ")} - {Address} - Class: {Class.Subject}";
        }
    }
}
