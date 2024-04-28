using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using FluentResults;

namespace Coffee.QR.Core.Services
{
    public class EventService : CrudService<EventDto, Event>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        

        public EventService(ICrudRepository<Event> crudRepository, IMapper mapper, IEventRepository eventRepository)
            : base(crudRepository, mapper)
        {
            _eventRepository = eventRepository;
        }

        public Result<EventDto> CreateEvent(EventDto eventDto)
        {
            try
            {
                // Create the event using the repository and receive an Event object.
                var eventt = _eventRepository.Create(new Event(eventDto.Name, eventDto.DateTime));

                // Convert the Event object to EventDto.
                EventDto resultDto = new EventDto
                {
                    Name = eventt.Name,
                    DateTime = eventt.DateTime
                };

                // Return a successful Result containing the EventDto.
                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                // Return a failed Result with the error message.
                return Result.Fail<EventDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


        public Task<Result<bool>> DeleteEventAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<EventDto>> GetEventByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<EventDto>> UpdateEventAsync(EventDto eventDto)
        {
            throw new NotImplementedException();
        }

    }
}
