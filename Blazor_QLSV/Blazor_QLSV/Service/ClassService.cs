﻿using Blazor_QLSV.Models.Entity;
using Blazor_QLSV.Models.Mapper;
using Blazor_QLSV.Service.Interface;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Share;

namespace Blazor_QLSV.Service
{
    public class ClassService : IClassService
    {
        ClassMapper classMapper = new ClassMapper();
        public Class GetClassById(int id)
        {
            try
            {
                IntGrpc intGrpc = new IntGrpc();
                intGrpc.Id = id;
                intGrpc.Empty = new Empty();
                var client = GetService();
                var classGrpc = client.GetClassById(intGrpc);
                Class @class = classMapper.ClassGrpcToClass(classGrpc);
                return @class;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Class> GetAllClasses()
        {
            try
            {
                List<Class> list = new List<Class>();
                var client = GetService();
                Empty empty = new Empty();

                var listGrpc = client.GetAllClasses(empty);
                foreach (var item in listGrpc.List)
                {
                    Class @class = classMapper.ClassGrpcToClass(item);
                    list.Add(@class);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ClassProto GetService()
        {
            try
            {
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                var channel = GrpcChannel.ForAddress($"http://localhost:5295", new GrpcChannelOptions { HttpHandler = httpHandler });
                return channel.CreateGrpcService<ClassProto>();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
