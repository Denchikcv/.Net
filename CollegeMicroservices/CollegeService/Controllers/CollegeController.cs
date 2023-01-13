using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CollegeService.Data;
using CollegeService.DTOs;
using CollegeService.Models;
using CollegeService.SyncDataServices.Http;
using CollegeService.AsyncDataServices;

namespace CollegeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly ICollegeRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public CollegesController(
            ICollegeRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CollegeReadDto>> GetColleges()
        {
            Console.WriteLine("--> Getting Colleges....");

            var CollegeItem = _repository.GetAllColleges();

            return Ok(_mapper.Map<IEnumerable<CollegeReadDto>>(CollegeItem));
        }

        [HttpGet("{id}", Name = "GetCollegeById")]
        public ActionResult<CollegeReadDto> GetCollegeById(int id)
        {
            var CollegeItem = _repository.GetCollegeById(id);
            if (CollegeItem != null)
            {
                return Ok(_mapper.Map<CollegeReadDto>(CollegeItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CollegeReadDto>> CreateCollege(CollegeCreateDto collegeCreateDto)
        {
            var CollegeModel = _mapper.Map<College>(collegeCreateDto);
            _repository.CreateCollege(CollegeModel);
            _repository.SaveChanges();

            var collegeReadDto = _mapper.Map<CollegeReadDto>(CollegeModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendCollegeToCommand(collegeReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async Message
            try
            {
                var collegePublishedDto = _mapper.Map<CollegePublishedDto>(collegeReadDto);
                collegePublishedDto.Event = "College_Published";
                _messageBusClient.PublishNewCollege(collegePublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetCollegeById), new { Id = collegeReadDto.Id}, collegeReadDto);
        }
    }
}