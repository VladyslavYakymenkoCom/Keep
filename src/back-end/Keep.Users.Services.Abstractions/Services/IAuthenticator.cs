using Keep.Users.Services.Abstractions.Dtos;
using System;

namespace Keep.Users.Services.Abstractions
{
    public interface IAuthenticator
    {
        UserContext AuthByCredentials(AuthByCredentialsDto dto);
    }
}
