namespace Blazor_QLSV.Models.Entity
{
    public class Student
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Address { get; set; }
        public virtual Class Class { get; set; }

        public Student(int iD, string name, DateTime dateOfBirth, string address, Class roomClass)
        {
            ID = iD;
            Name = name;
            DateOfBirth = dateOfBirth;
            Address = address;
            Class = roomClass;
        }
        public Student(string name, DateTime dateOfBirth, string address, Class roomClass)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Address = address;
            Class = roomClass;
        }
        public Student()
        {
        }
        public override string? ToString()
        {
            return $"{ID} - {Name} - {DateOfBirth.ToString("yyyy/mm/đ")} - {Address} - Class: {Class.Subject}";
        }
    }
}