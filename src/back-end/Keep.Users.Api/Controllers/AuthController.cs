using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Keep.Users.Services.Abstractions;
using Keep.Users.Services.Abstractions.Dtos;
using Keep.Users.Services.Abstractions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Keep.Users.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        #region Private fields
        private readonly IAuthenticator _authenticator; 
        #endregion

        public AuthController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpGet, Route("me")]
        public string CheckAuth()
        {
            return "Authenticated";
        }

        [HttpGet, Route("token")]
        public IActionResult AuthByCredentials([FromBody]AuthByCredentialsDto dto)
        {
            return new JsonResult(_authenticator.AuthByCredentials(dto));
        }
    }
}
