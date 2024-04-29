using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IAuthService
    {
        Result<AuthenticationTokensDto> Login(LoginDto credentials);
        Result<AuthenticationTokensDto> Register(RegisterDto account);
    }
}
