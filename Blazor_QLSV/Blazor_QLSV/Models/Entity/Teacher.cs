﻿namespace Blazor_QLSV.Models.Entity
{
    public class Teacher
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public Teacher(int ID, string Name, DateTime DateOfBirth)
        {
            this.ID = ID;
            this.Name = Name;
            this.DateOfBirth = DateOfBirth;
        }
        public Teacher()
        {

        }
    }
}