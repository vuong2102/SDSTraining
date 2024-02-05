using Blazor_QLSV.Models.Entity;

namespace Blazor_QLSV.Service.Interface
{
    public interface IClassService
    {
        Class GetClassById(int id);
        public List<Class> GetAllClasses();

    }
}
