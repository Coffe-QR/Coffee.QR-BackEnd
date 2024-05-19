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
        private readonly IItemRepository _itemRepository;

        public MenuItemService(ICrudRepository<MenuItem> crudRepository, IMapper mapper, IMenuItemRepository menuItemRepository, IItemRepository itemRepository)
            : base(crudRepository,mapper)
        {
            _menuItemRepository = menuItemRepository;
            _itemRepository = itemRepository;
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

        public Result<List<ItemDto>> GetAllForMenu(long menuId) 
        {
            try
            {
                List<Item> items = new List<Item>();
                List<MenuItem> menuItems = _menuItemRepository.GetAllByMenuId(menuId);
                foreach (var menuItem in menuItems)
                {
                    items.Add(_itemRepository.GetById(menuItem.ItemId));
                }

                var itemDtos = items.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), i.Type.ToString(), true),
                    Price = i.Price,
                    Picture = i.Picture,
                }).ToList();

                return Result.Ok(itemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ItemDto>>("Failed to retrieve items").WithError(e.Message);
            }
        }

        public Result<List<ItemDto>> GetAllFoodForMenu(long menuId)
        {
            try
            {
                List<Item> items = new List<Item>();
                List<MenuItem> menuItems = _menuItemRepository.GetAllByMenuId(menuId);
                foreach (var menuItem in menuItems)
                {
                    items.Add(_itemRepository.GetById(menuItem.ItemId));
                }
                List<Item> foods = new List<Item>();
                foreach (var item in items) 
                {
                    if (item.Type == ItemType.FOOD) 
                    {
                        foods.Add(item);
                    }
                }

                var itemDtos = foods.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), i.Type.ToString(), true),
                    Price = i.Price,
                    Picture = i.Picture,
                }).ToList();

                return Result.Ok(itemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ItemDto>>("Failed to retrieve items").WithError(e.Message);
            }
        }

        public Result<List<ItemDto>> GetAllDrinksForMenu(long menuId)
        {
            try
            {
                List<Item> items = new List<Item>();
                List<MenuItem> menuItems = _menuItemRepository.GetAllByMenuId(menuId);
                foreach (var menuItem in menuItems)
                {
                    items.Add(_itemRepository.GetById(menuItem.ItemId));
                }
                List<Item> drinks = new List<Item>();
                foreach (var item in items)
                {
                    if (item.Type == ItemType.DRINK)
                    {
                        drinks.Add(item);
                    }
                }

                var itemDtos = drinks.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), i.Type.ToString(), true),
                    Price = i.Price,
                    Picture = i.Picture,
                }).ToList();

                return Result.Ok(itemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ItemDto>>("Failed to retrieve items").WithError(e.Message);
            }
        }

        public Result<List<ItemDto>> GetAllNotOnMenu(long menuId)
        {
            try
            {
                List<Item> items = new List<Item>();
                List<MenuItem> menuItems = _menuItemRepository.GetAllByMenuId(menuId);
                foreach (var menuItem in menuItems)
                {
                    items.Add(_itemRepository.GetById(menuItem.ItemId));
                }

                List<Item> itemsReturn = new List<Item>();
                List<Item> allItems = _itemRepository.GetAll();

                foreach (var item in allItems) 
                {
                    int flag = 0;
                    foreach (var menuItem in menuItems) 
                    {
                        if (menuItem.ItemId == item.Id || item.Type== ItemType.INGREDIENT) 
                        {
                            flag = 1;
                        }
                    }
                    if (flag == 0) 
                    {
                        itemsReturn.Add(item);
                    }
                }

                var itemDtos = itemsReturn.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), i.Type.ToString(), true),
                    Price = i.Price,
                    Picture = i.Picture,
                }).ToList();

                return Result.Ok(itemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ItemDto>>("Failed to retrieve items").WithError(e.Message);
            }
        }


        public bool DeleteMenuItem(long menuItemId)
        {
            var menuItemToDelete = _menuItemRepository.Delete(menuItemId);
            return menuItemToDelete != null;
        }

        public bool DeleteByMenuIdAndItemId(long menuId,long itemId)
        {
            var menuItemToDelete = _menuItemRepository.DeleteByMenuIdAndItemId(menuId, itemId);
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
