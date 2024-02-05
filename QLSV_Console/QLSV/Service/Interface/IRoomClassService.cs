using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Manager.Interface
{
    internal interface IRoomClassService
    {
        List<RoomClass> getListClasses();
        void ShowClasses(List<RoomClass> rooms);
    }
}
