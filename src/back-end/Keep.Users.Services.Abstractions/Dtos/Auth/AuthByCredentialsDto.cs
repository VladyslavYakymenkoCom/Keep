using System;
using System.Collections.Generic;
using System.Text;

namespace Keep.Users.Services.Abstractions.Dtos
{
    public class AuthByCredentialsDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
