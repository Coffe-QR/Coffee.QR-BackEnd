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
    public class MenuRegionService : CrudService<MenuRegionDto, MenuRegion>, IMenuRegionService
    {
        private readonly IMenuRegionRepository _menuRegionRepository;


        public MenuRegionService(ICrudRepository<MenuRegion> crudRepository, IMapper mapper, IMenuRegionRepository menuRegionRepository)
            : base(crudRepository, mapper)
        {
            _menuRegionRepository = menuRegionRepository;
        }

        public Result<MenuRegionDto> CreateMenuRegion(MenuRegionDto menuRegionDto)
        {
            try
            {
                var menuRegiont = _menuRegionRepository.Create(new MenuRegion(menuRegionDto.Name, menuRegionDto.MenuId));

                MenuRegionDto resultDto = new MenuRegionDto
                {
                    Id = menuRegiont.Id,
                    Name = menuRegiont.Name,
                    MenuId = menuRegiont.MenuId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<MenuRegionDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<MenuRegionDto>> GetAllMenuRegions()
        {
            try
            {
                var menuRegions = _menuRegionRepository.GetAll();
                var menuRegionDtos = menuRegions.Select(mr => new MenuRegionDto
                {
                    Id = mr.Id,
                    Name = mr.Name,
                    MenuId = mr.MenuId,
                }).ToList();

                return Result.Ok(menuRegionDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<MenuRegionDto>>("Failed to retrieve menu regions").WithError(e.Message);
            }
        }


        public bool DeleteMenuRegion(long menuRegionId)
        {
            var menuRegionToDelete = _menuRegionRepository.Delete(menuRegionId);
            return menuRegionToDelete != null;
        }

        public Result<List<MenuRegionDto>> GetAllByMenuId(long menuId)
        {
            try
            {
                var menuRegions = _menuRegionRepository.GetAllByMenuId(menuId);
                var menuRegionDtos = menuRegions.Select(mr => new MenuRegionDto
                {
                    Id = mr.Id,
                    Name = mr.Name,
                    MenuId = mr.MenuId,
                }).ToList();

                return Result.Ok(menuRegionDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<MenuRegionDto>>("Failed to retrieve menu regions for local").WithError(e.Message);
            }
        }

        public Result<MenuRegionDto> GetById(long menuRegionId)
        {
            try
            {
                MenuRegion menuRegion = _menuRegionRepository.GetById(menuRegionId);
                if (menuRegion != null)
                {
                    MenuRegionDto menuRegionDto = new MenuRegionDto
                    {
                        Id = menuRegion.Id,
                        Name = menuRegion.Name,
                        MenuId = menuRegion.MenuId
                    };
                    return Result.Ok(menuRegionDto);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return Result.Fail<MenuRegionDto>("Failed to retrieve menu region").WithError(e.Message);
            }
        }

        public bool UpdateMenuRegion(MenuRegionDto newMenuRegion)
        {
            MenuRegion oldMenuRegion = _menuRegionRepository.GetById(newMenuRegion.Id);
            oldMenuRegion.Name = newMenuRegion.Name;
            return _menuRegionRepository.UpdateMenuRegion(oldMenuRegion);
        }
    }
}
