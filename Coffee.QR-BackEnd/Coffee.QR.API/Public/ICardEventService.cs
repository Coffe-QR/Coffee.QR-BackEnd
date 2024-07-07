using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ICardEventService
    {
        Result<CardEventDto> CreateCardEvent(CardEventDto cardDto);
        bool DeleteCardEvent(long cardEventId);
        Result<List<CardEventDto>> GetAllByEventId(long eventId);
        Result<List<CardEventDto>> GetAllCardEvents();
        Task<CardEventDto> GetByIdAsync(long id);
    }
}
