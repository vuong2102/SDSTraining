

namespace QLSV.Models
{
    public class Student{
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Address { get; set; }
        public virtual RoomClass RoomClass { get; set; }

        public Student(int iD, string name, DateTime dateOfBirth, string address, RoomClass roomClass)
        {
            this.ID = iD;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.RoomClass = roomClass;
        }
        public Student(string name, DateTime dateOfBirth, string address, RoomClass roomClass)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.RoomClass = roomClass;
        }
        public Student()
        {
        }
        public override string? ToString()
        {
            return $"{ID} - {Name} - {DateOfBirth.ToString("yyyy/mm/đ")} - {Address} - Class: {RoomClass.Subject}";
        }
    }
}
