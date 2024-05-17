using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;

namespace Coffee.QR.Core.Mappers
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDto>().ReverseMap();
        }

    }
}
