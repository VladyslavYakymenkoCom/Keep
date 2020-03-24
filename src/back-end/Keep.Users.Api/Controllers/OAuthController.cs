using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keep.Users.Api.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class OAuthController : ControllerBase
    { 
        private readonly ILogger<OAuthController> _logger;

        public OAuthController(ILogger<OAuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello from User API";
        }
    }
}
