using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.CollegePublished:
                    addCollege(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage)!;

            switch(eventType.Event)
            {
                case "College_Published":
                    Console.WriteLine("--> College Published Event Detected");
                    return EventType.CollegePublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addCollege(string collegePublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                
                var collegePublishedDto = JsonSerializer.Deserialize<CollegePublishedDto>(collegePublishedMessage);

                try
                {
                    var plat = _mapper.Map<College>(collegePublishedDto);
                    if(!repo.ExternalCollegeExists(plat.ExternalID))
                    {
                        repo.CreateCollege(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> College added!");
                    }
                    else
                    {
                        Console.WriteLine("--> College already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add College to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        CollegePublished,
        Undetermined
    }
}