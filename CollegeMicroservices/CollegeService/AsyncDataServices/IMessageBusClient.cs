using CollegeService.DTOs;

namespace CollegeService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewCollege(CollegePublishedDto collegePublishedDto);
    }
}