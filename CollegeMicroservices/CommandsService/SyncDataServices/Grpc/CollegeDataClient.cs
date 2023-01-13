using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using CollegeService;


namespace CommandsService.SyncDataServices.Grpc
{
    public class CollegeDataClient : ICollegeDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CollegeDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<College> ReturnAllColleges()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCollege"]!}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcCollege"]!);
            var client = new GrpcCollege.GrpcCollegeClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllColleges(request);
                return _mapper.Map<IEnumerable<College>>(reply.College);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null!;
            }
        }
    }

}