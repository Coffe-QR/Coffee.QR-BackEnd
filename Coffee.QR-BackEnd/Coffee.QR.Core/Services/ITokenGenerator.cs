using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public interface ITokenGenerator
    {
        Result<AuthenticationTokensDto> GenerateAccessToken(User user);
    }
}
