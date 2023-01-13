using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using CollegeService.Data;

namespace CollegeService.SyncDataServices.Grpc
{
    public class GrpcCollegeService : GrpcCollege.GrpcCollegeBase
    {
        private readonly ICollegeRepo _repository;
        private readonly IMapper _mapper;

        public GrpcCollegeService(ICollegeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<CollegeResponse> GetAllColleges(GetAllRequest request, ServerCallContext context)
        {
            var response = new CollegeResponse();
            var platforms = _repository.GetAllColleges();

            foreach(var plat in platforms)
            {
                response.College.Add(_mapper.Map<GrpcCollegeModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}