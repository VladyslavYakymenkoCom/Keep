using System;
using System.Collections.Generic;
using System.Text;

namespace Keep.Users.Services.Abstractions.Dtos 
{
    public class UserContext
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
