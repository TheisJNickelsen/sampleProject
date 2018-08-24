using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.DTOs;
using SampleSolution.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using SampleSolution.Models.ElasticSearch;

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
            var userStream = _userStreamReadRepository.GetByQuery(query, limit);

            return userStream.ToList();
        }
    }
}