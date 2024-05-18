using Coffee.QR.API.DTOs;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ICardService
    {
        Result<CardDto> CreateCard(CardDto cardDto);
        Result<List<CardDto>> GetAllCards();
        bool DeleteCard(long cardId);
        Result<List<CardDto>> GetAllByEventId(long userId);
        Task<CardDto> GetByIdAsync(long id);
    }
}
