using Coffee.QR.API.DTOs;
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
        Task<Result<EventDto>> GetEventByIdAsync(long id);
        Task<Result<EventDto>> UpdateEventAsync(EventDto eventDto);
        Task<Result<bool>> DeleteEventAsync(long id);
    }
}
