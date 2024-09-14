using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Mappers
{
    public class MenuRegionProfile : Profile
    {
        public MenuRegionProfile()
        {
            CreateMap<MenuRegion, MenuRegionDto>().ReverseMap();
        }
    }
}
