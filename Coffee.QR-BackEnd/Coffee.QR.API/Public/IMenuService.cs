using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IMenuService
    {
        Result<MenuDto> CreateMenu(MenuDto menuDto);
        Result<List<MenuDto>> GetAllMenus();
        bool DeleteMenu(long menuId);
        Task<Result<MenuDto>> GetMenuByIdAsync(long id);
        Task<Result<MenuDto>> UpdateMenuAsync(MenuDto menuDto);
        Result<List<MenuDto>> GetAllByLocalId(long localId);
        Result<MenuDto> GetById(long menuId);
        bool UpdateMenu(MenuDto menu);
    }
}
