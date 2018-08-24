using SampleSolution.Common.Domain.Commands;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.DTOs;
using SampleSolution.Helpers;
using SampleSolution.Mappers;
using SampleSolution.Models.Facebook;
using SampleSolution.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SampleSolution.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommandBus _commandBus;

        public AccountsController(IUserService userService, ICommandBus commandBus)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdentity = ApplicationUserMapper.RegistrationDtoToApplicationUser(dto);

            var result = await _userService.CreateUserAsync(userIdentity, dto.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            var createCommand =
                new CreateBusinessUserCommand(Guid.NewGuid(), 
                    new IdentityId(userIdentity.Id), 
                    dto.Location, 
                    null, 
                    null,
                    new Email(userIdentity.Email), 
                    userIdentity.FirstName,
                    null,
                    userIdentity.LastName);

            await _commandBus.Send(createCommand);

            return new OkObjectResult(new ResponseObject { Message = "Account Created"});
        }
    }
}