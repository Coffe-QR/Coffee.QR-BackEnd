using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class MenuItemService : CrudService<MenuItemDto, MenuItem>, IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(ICrudRepository<MenuItem> crudRepository, IMapper mapper, IMenuItemRepository menuItemRepository)
            : base(crudRepository,mapper)
        {
            _menuItemRepository = menuItemRepository;
        }

        public Result<MenuItemDto> CreateMenuItem(MenuItemDto menuItemDto)
        {
            try
            {
                var menuItemt = _menuItemRepository.Create(new MenuItem(menuItemDto.MenuId, menuItemDto.ItemId));

                MenuItemDto resultDto = new MenuItemDto
                {
                    Id = menuItemt.Id,
                    MenuId = menuItemt.MenuId,
                    ItemId = menuItemt.ItemId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<MenuItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<MenuItemDto>> GetAllMenuItems()
        {
            try
            {
                var menuItems = _menuItemRepository.GetAll();
                var menuItemDtos = menuItems.Select(m => new MenuItemDto
                {
                    Id = m.Id,
                    MenuId = m.MenuId,
                    ItemId = m.ItemId,
                }).ToList();

                return Result.Ok(menuItemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<MenuItemDto>>("Failed to retrieve menuItems").WithError(e.Message);
            }
        }


        public bool DeleteMenuItem(long menuItemId)
        {
            var menuItemToDelete = _menuItemRepository.Delete(menuItemId);
            return menuItemToDelete != null;
        }


        public Task<Result<MenuItemDto>> GetMenuItemByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MenuItemDto>> UpdateMenuItemAsync(MenuItemDto menuItemDto)
        {
            throw new NotImplementedException();
        }
    }
}
