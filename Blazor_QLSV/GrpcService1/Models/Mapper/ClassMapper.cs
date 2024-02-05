using GrpcService1.Models;
using GrpcService1.Models.Entity;
using Share;
namespace GrpcService1.Models.Mapper
{
    public class ClasssMapper
    {
        public ClassGrpc ClassToClassGrpc(Class _class)
        {
            ClassGrpc classGrpc = new ClassGrpc();
            classGrpc.Id = _class.ID;
            classGrpc.ClassName = _class.Name;
            classGrpc.Subject = _class.Subject;
            return classGrpc;
        }
        public Class ClassGrpcToClass(ClassGrpc classGrpc)
        {
            Class _class = new Class();
            _class.ID = classGrpc.Id;
            _class.Name = classGrpc.ClassName;
            _class.Subject = classGrpc.Subject;
            return _class;
        }
    }
}
