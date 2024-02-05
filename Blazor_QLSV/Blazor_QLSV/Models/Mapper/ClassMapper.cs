﻿using Blazor_QLSV.Models.Entity;
using Share;
namespace Blazor_QLSV.Models.Mapper
{
    public class ClassMapper
    {
        public ClassGrpc ClassToClassGrpc(Class @class)
        {
            ClassGrpc classGrpc = new ClassGrpc();
            classGrpc.Id = @class.ID;
            classGrpc.ClassName = @class.Name;
            classGrpc.Subject = @class.Subject;
            return classGrpc;
        }

        public Class ClassGrpcToClass(ClassGrpc classGrpc)
        {
            Class @class = new Class();
            @class.ID = classGrpc.Id;
            @class.Name = classGrpc.ClassName;
            @class.Subject = classGrpc.Subject;
            return @class;
        }
    }
}
