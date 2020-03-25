using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keep.Users.Services.Abstractions.Options
{
    public class AuthOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }

        public SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
