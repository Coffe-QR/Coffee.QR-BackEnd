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
                var eventt = _eventRepository.Create(new Event(eventDto.Name, eventDto.DateTime,eventDto.Description,eventDto.Image,eventDto.UserId,eventDto.LocalId));

                EventDto resultDto = new EventDto
                {
                    Name = eventt.Name,
                    DateTime = eventt.DateTime,
                    Description = eventt.Description,
                    Image = eventt.Image,
                    UserId = eventt.UserId,
                    LocalId = eventt.LocalId
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<EventDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<EventDto>> GetAllEvents()
        {
            try
            {
                var events = _eventRepository.GetAll();
                var eventDtos = events.Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    DateTime = e.DateTime,
                    Description = e.Description,
                    Image = e.Image,
                    UserId = e.UserId,
                    LocalId = e.LocalId,
                }).ToList();

                return Result.Ok(eventDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<EventDto>>("Failed to retrieve events").WithError(e.Message);
            }
        }


        public bool DeleteEvent(long eventId)
        {
            var eventToDelete = _eventRepository.Delete(eventId);
            return eventToDelete != null;
        }

        public Result<List<EventDto>> GetAllByUserId(long userId)
        {
            try
            {
                var events = _eventRepository.GetAllByUserId(userId);
                var eventDtos = events.Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    DateTime = e.DateTime,
                    Description = e.Description,
                    Image = e.Image,
                    UserId = e.UserId,
                    LocalId = e.LocalId,
                }).ToList();

                return Result.Ok(eventDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<EventDto>>("Failed to retrieve events for user").WithError(e.Message);
            }
        }

        public async Task<EventDto> GetByIdAsync(long id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null)
                return null;

            return new EventDto
            {
                Id = @event.Id,
                Name = @event.Name,
                DateTime = @event.DateTime,
                Description = @event.Description,
                Image = @event.Image,
                UserId = @event.UserId,
                LocalId = @event.LocalId
            };
        }


        public Task<Result<EventDto>> UpdateEventAsync(EventDto eventDto)
        {
            throw new NotImplementedException();
        }

    }
}
