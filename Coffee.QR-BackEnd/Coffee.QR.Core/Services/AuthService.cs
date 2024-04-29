using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(IUserRepository userRepository,ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public Result<AuthenticationTokensDto> Login(LoginDto credentials)
        {
            var user = _userRepository.GetActiveByName(credentials.Username);
            if (user == null || credentials.Password != user.Password) return Result.Fail(FailureCode.NotFound);

            return _tokenGenerator.GenerateAccessToken(user);
        }

        public Result<AuthenticationTokensDto> Register(RegisterDto account)
        {
//          if (account.Role == UserRoleDto.Administrator) return Result.Fail(FailureCode.InvalidArgument);
            if (_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);
            //UserRole role = UserRole.Client; // UserRole role = account.Role == UserRoleDto.Manager? UserRole.Manager : UserRole.Client;


            try
            {
                var user = _userRepository.Create(new User(account.Username, account.Email, account.Password, account.FirstName, account.LastName, (UserRole)account.Role, true));

                return _tokenGenerator.GenerateAccessToken(user);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
