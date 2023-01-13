using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/colleges/{collegeId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForCollege(int collegeId)
        {
            Console.WriteLine($"--> Hit GetCommandsForCollege: {collegeId}");

            if (!_repository.PlaformExits(collegeId))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForCollege(collegeId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForCollege")]
        public ActionResult<CommandReadDto> GetCommandForCollege(int collegeId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForCollege: {collegeId} / {commandId}");

            if (!_repository.PlaformExits(collegeId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(collegeId, commandId);

            if(command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForCollege(int collegeId, CommandCreateDto commandDto)
        {
             Console.WriteLine($"--> Hit CreateCommandForCollege: {collegeId}");

            if (!_repository.PlaformExits(collegeId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _repository.CreateCommand(collegeId, command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForCollege),
                new {collegeId = collegeId, commandId = commandReadDto.Id}, commandReadDto);
        }

    }
}