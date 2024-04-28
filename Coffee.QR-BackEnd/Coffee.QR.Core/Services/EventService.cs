using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class EventService : CrudService<EventDto, Event>, IEventService
    {
        private readonly IEventRepository EventRepository;
        public EventService(ICrudRepository<Event> crudRepository, IMapper mapper, IEventRepository eventRepository) : base(crudRepository, mapper)
        {
            EventRepository = eventRepository;
        }


    }
}
