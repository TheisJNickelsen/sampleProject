using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.Common.Domain.Commands;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.DTOs;
using SampleSolution.Helpers;
using SampleSolution.Repositories;
using SampleSolution.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleSolution.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class SomeDataController : Controller
    {
        private readonly ISomeDataReadService _someDataReadService;
        private readonly ICommandBus _commandBus;
        private readonly IUserService _userService;
        private readonly IBusinessUserRepositoy _businessUserRepositoy;

        public SomeDataController(ISomeDataReadService someDataReadService, 
            ICommandBus commandBus,
            IUserService userService,
            IBusinessUserRepositoy businessUserRepositoy)
        {
            _someDataReadService = someDataReadService ?? throw new ArgumentNullException(nameof(someDataReadService));
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _businessUserRepositoy = businessUserRepositoy ?? throw new ArgumentNullException(nameof(businessUserRepositoy));
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
            var someData = mapper.Map<List<SomeAggregate>, List<SomeDataDto>>(myData);

            return someData;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSomeData([FromBody]CreateSomeDataDto createSomeDataModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = HttpContextHelpers.GetCurrentUserEmail(User);
            var user = await _userService.FindByEmailAsync(userEmail);

            var createCommand = new CreateSomeDataCommand(Guid.NewGuid(),
                createSomeDataModel.FirstName,
                createSomeDataModel.MiddleName,
                createSomeDataModel.LastName,
                createSomeDataModel.Title,
                new Color(createSomeDataModel.Color),
                DateTime.Now,
                new FacebookUrl(createSomeDataModel.FacebookUrl),
                new ApplicationUserId(user.Id));

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

        [HttpPost("share")]
        public async Task<IActionResult> ShareContact([FromBody] ShareDto shareDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = HttpContextHelpers.GetCurrentUserEmail(User);
            var user = await _userService.FindByEmailAsync(userEmail);
            var shareKardCommand = new ShareContactCommand(shareDto.ContactId, shareDto.RecipientUserId, user.Id);

            await _commandBus.Send(shareKardCommand);

            return Ok();
        }

    }
}