using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using SampleSolution.Common.Domain.Commands;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.DTOs;
using SampleSolution.Helpers;
using SampleSolution.Services;
using Microsoft.AspNetCore.Authorization;

namespace SampleSolution.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class SomeDataController : Controller
    {
        private readonly ISomeDataReadService _someDataReadService;
        private readonly ICommandBus _commandBus;
        private readonly IUserService _userService;

        public SomeDataController(ISomeDataReadService someDataReadService, 
            ICommandBus commandBus,
            IUserService userService)
        {
            _someDataReadService = someDataReadService ?? throw new ArgumentNullException(nameof(someDataReadService));
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public List<SomeDataDto> SomeData()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Data.Contexts.Models.SomeData, SomeAggregate>();
                cfg.CreateMap<Color, string>().ConvertUsing(l => l.Value);
                cfg.CreateMap<FacebookUrl, string>().ConvertUsing(l => l.Value);
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();

            var userEmail = HttpContextHelpers.GetCurrentUserEmail(User);
            var myData = _someDataReadService.GetSomeData(userEmail);
            var SampleSolutionDto = mapper.Map<List<SomeAggregate>, List<SomeDataDto>>(myData);

            return SampleSolutionDto;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSomeData([FromBody]CreateSomeDataDto createSomeDataModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = HttpContextHelpers.GetCurrentUserEmail(User);

            var createCommand = new CreateSomeDataCommand(Guid.NewGuid(),
                createSomeDataModel.FirstName,
                createSomeDataModel.MiddleName,
                createSomeDataModel.LastName,
                createSomeDataModel.Title,
                new Color(createSomeDataModel.Color),
                DateTime.Now,
                new FacebookUrl(createSomeDataModel.FacebookUrl),
                userEmail);

            await _commandBus.Send(createCommand);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSomeData(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteSomeDataCommand = new DeleteSomeDataCommand(id);

            await _commandBus.Send(deleteSomeDataCommand);
            
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSomeData([FromBody] SomeDataDto someDataDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateSomeDataCommand = new UpdateSomeDataCommand(someDataDto.Id,
                someDataDto.FirstName,
                someDataDto.MiddleName,
                someDataDto.LastName,
                someDataDto.Title,
                new Color(someDataDto.Color),
                DateTime.Now,
                new FacebookUrl(someDataDto.FacebookUrl));

            await _commandBus.Send(updateSomeDataCommand);

            return Ok();
        }
    }
}