using GrpcService1.Models.Entity;

namespace GrpcService1.Repository.Interface
{
    public interface IClassRepository
    {
        Class GetClassById(int id);
        List<Class> GetListClass();
    }
}