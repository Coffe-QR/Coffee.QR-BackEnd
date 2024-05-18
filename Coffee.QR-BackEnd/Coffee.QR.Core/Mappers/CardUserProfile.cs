using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;

namespace Coffee.QR.Core.Mappers
{
    public class CardUserProfile : Profile
    {
        public CardUserProfile()
        {
            CreateMap<CardUser, CardUserDto>().ReverseMap();
        }
    }
}
