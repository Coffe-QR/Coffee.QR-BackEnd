using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;

namespace Coffee.QR.Core.Mappers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
