using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IMenuRegionService
    {
        Result<MenuRegionDto> CreateMenuRegion(MenuRegionDto menuRegionDto);
        Result<List<MenuRegionDto>> GetAllMenuRegions();
        bool DeleteMenuRegion(long menuRegionId);
        Result<List<MenuRegionDto>> GetAllByMenuId(long menuId);
        Result<MenuRegionDto> GetById(long menuRegionId);
        bool UpdateMenuRegion(MenuRegionDto newMenuRegion);
    }
}
