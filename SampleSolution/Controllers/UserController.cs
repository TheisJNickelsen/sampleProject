using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.Models.ElasticSearch;
using SampleSolution.Repositories;
using System;
using System.Collections.Generic;

namespace SampleSolution.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserStreamReadRepository _userStreamReadRepository;

        public UserController(IUserStreamReadRepository userStreamReadRepository)
        {
            _userStreamReadRepository = userStreamReadRepository ?? throw new ArgumentNullException(nameof(userStreamReadRepository));
        }

        // GET: api/CreditData/GetByRegistration/33514322
        //[HttpGet("GetByRegistration/{query}", Name= "GetByRegistration")]

        [HttpGet("search", Name = "search")]
        public List<UserStream> SearchUsers([FromQuery] string query, [FromQuery] int limit)
        {
            return _userStreamReadRepository.GetByQuery(query, limit);
        }
    }
}