using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.DTOs;
using SampleSolution.Helpers;
using System.Collections.Generic;

namespace SampleSolution.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public UserController()
        {
        }

        // GET: api/CreditData/GetByRegistration/33514322
        //[HttpGet("GetByRegistration/{query}", Name= "GetByRegistration")]

        [HttpGet("search", Name = "search")]
        public List<StreamResult> SearchUsers([FromQuery] string query, [FromQuery] int limit)
        {
            var userEmail = HttpContextHelpers.GetCurrentUserEmail(User);

            return new List<StreamResult>();
        }
    }
}