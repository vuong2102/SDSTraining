using QLSV.Manager.Interface;
using QLSV.Models;
using QLSV.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Manager
{
    class RoomClassService : IRoomClassService
    {
        private readonly IClassRepository _classRepository;
        public RoomClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        public List<RoomClass> getListClasses()
        {
            return _classRepository.GetListClass();
        }
        public void ShowClasses(List<RoomClass> rooms) {
            Console.WriteLine("***************************************************");
            Console.WriteLine("{0, -5} {1, -20} {2, -10} {3, 5}", "ID", "Class", "Subject", "Teacher");
            if(rooms.Count > 0 && rooms != null) 
            {
                foreach(RoomClass room in rooms)
                {
                    Console.WriteLine("{0, -5} {1, -20} {2, -10} {3, 5}", room.ID, room.Name, room.Subject, room.Teacher.Name);
                }
            }
            

        }
    }
}
