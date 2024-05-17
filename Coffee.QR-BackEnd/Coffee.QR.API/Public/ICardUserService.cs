using System.Collections.Generic;
using System.Threading.Tasks;
using Coffee.QR.API.DTOs;
using FluentResults;

namespace Coffee.QR.Core.Interfaces
{
    public interface ICardUserService
    {
        Result<CardUserDto> CreateCardUser(CardUserDto cardUserDto);
        Task<bool> DeleteCardUserAsync(long cardUserId);
        Task<Result<List<CardUserDto>>> GetAllCardUsersAsync();
        Task<Result<CardUserDto>> GetCardUserByIdAsync(long cardUserId);
        Task UpdateCardUserAsync(CardUserDto cardUserDto);

    }
}
