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
        bool DeleteByMenuIdAndItemId(long menuId, long itemId);
        Result<List<ItemDto>> GetAllForMenu(long menuId);
        Result<List<ItemDto>> GetAllFoodForMenu(long menuId);
        Result<List<ItemDto>> GetAllDrinksForMenu(long menuId);
        Result<List<ItemDto>> GetAllNotOnMenu(long menuId);

    }
}
