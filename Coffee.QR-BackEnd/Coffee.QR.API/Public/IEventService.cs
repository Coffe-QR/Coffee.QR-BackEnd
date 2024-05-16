using Coffee.QR.API.DTOs;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IEventService
    {
        Result<EventDto> CreateEvent(EventDto eventDto);
        Result<List<EventDto>> GetAllEvents();
        bool DeleteEvent(long eventId);
        Task<EventDto> GetByIdAsync(long id);
        Task<Result<EventDto>> UpdateEventAsync(EventDto eventDto);
        Result<List<EventDto>> GetAllByUserId(long userId);
    }
}
