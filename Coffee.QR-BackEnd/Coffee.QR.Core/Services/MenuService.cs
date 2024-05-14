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
    public class MenuService : CrudService<MenuDto, Menu>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;


        public MenuService(ICrudRepository<Menu> crudRepository, IMapper mapper, IMenuRepository menuRepository)
            : base(crudRepository, mapper)
        {
            _menuRepository = menuRepository;
        }

        public Result<MenuDto> CreateMenu(MenuDto menuDto)
        {
            try
            {
                var menut = _menuRepository.Create(new Menu(menuDto.StartDate, menuDto.EndDate, (MenuStatus)Enum.Parse(typeof(MenuStatus), menuDto.Status.ToString(), true), menuDto.CafeId));

                MenuDto resultDto = new MenuDto
                {
                    Id = menut.Id,
                    StartDate = menut.StartDate,
                    EndDate = menut.EndDate,  
                    Status = (MenuStatusDto)Enum.Parse(typeof(MenuStatusDto), menuDto.Status.ToString(),true),  
                    CafeId = menut.CafeId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<MenuDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<MenuDto>> GetAllMenus()
        {
            try
            {
                var menus = _menuRepository.GetAll();
                var menuDtos = menus.Select(m => new MenuDto
                {
                    Id = m.Id,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Status = (MenuStatusDto)Enum.Parse(typeof(MenuStatusDto), m.Status.ToString(), true),
                    CafeId = m.CafeId,
                }).ToList();

                return Result.Ok(menuDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<MenuDto>>("Failed to retrieve menus").WithError(e.Message);
            }
        }


        public bool DeleteMenu(long menuId)
        {
            var menuToDelete = _menuRepository.Delete(menuId);
            return menuToDelete != null;
        }


        public Task<Result<MenuDto>> GetMenuByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MenuDto>> UpdateMenuAsync(MenuDto menuDto)
        {
            throw new NotImplementedException();
        }
    }
}
