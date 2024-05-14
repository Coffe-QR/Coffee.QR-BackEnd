using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ILocalUserService
    {
        Result<LocalUserDto> CreateLocalUser(LocalUserDto localUserDto);
        Result<List<LocalUserDto>> GetAllLocalUsers();
        bool DeleteLocalUser(long localUserId);
        Task<Result<LocalUserDto>> GetLocalUserByIdAsync(long id);
        Task<Result<LocalUserDto>> UpdateLocalUserAsync(LocalUserDto localUserDto);
        Result<LocalUserDto> GetByUserId(long userId);
    }
}
