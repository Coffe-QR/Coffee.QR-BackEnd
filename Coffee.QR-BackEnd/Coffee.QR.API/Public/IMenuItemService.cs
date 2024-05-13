using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IMenuItemService
    {
        Result<MenuItemDto> CreateMenuItem(MenuItemDto menuItemDto);
        Result<List<MenuItemDto>> GetAllMenuItems();
        bool DeleteMenuItem(long menuItemId);
        Task<Result<MenuItemDto>> GetMenuItemByIdAsync(long id);
        Task<Result<MenuItemDto>> UpdateMenuItemAsync(MenuItemDto menuItemDto);

    }
}
