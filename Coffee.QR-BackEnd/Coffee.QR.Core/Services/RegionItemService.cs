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
    public class RegionItemService : CrudService<RegionItemDto, RegionItem>, IRegionItemService
    {
        private readonly IRegionItemRepository _regionItemRepository;
        private readonly IItemRepository _itemRepository;

        public RegionItemService(ICrudRepository<RegionItem> crudRepository, IMapper mapper, IRegionItemRepository regionItemRepository, IItemRepository itemRepository)
            : base(crudRepository, mapper)
        {
            _regionItemRepository = regionItemRepository;
            _itemRepository = itemRepository;
        }

        public Result<RegionItemDto> CreateRegionItem(RegionItemDto regionItemDto)
        {
            try
            {
                var regionItemt = _regionItemRepository.Create(new RegionItem(regionItemDto.RegionId, regionItemDto.ItemId));

                RegionItemDto resultDto = new RegionItemDto
                {
                    Id = regionItemt.Id,
                    RegionId = regionItemt.RegionId,
                    ItemId = regionItemt.ItemId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<RegionItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<RegionItemDto>> GetAllRegionItems()
        {
            try
            {
                var regionItems = _regionItemRepository.GetAll();
                var regionItemDtos = regionItems.Select(r => new RegionItemDto
                {
                    Id = r.Id,
                    RegionId = r.RegionId,
                    ItemId = r.ItemId,
                }).ToList();

                return Result.Ok(regionItemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<RegionItemDto>>("Failed to retrieve regionItems").WithError(e.Message);
            }
        }

        public Result<List<ItemDto>> GetAllForRegion(long regionId)
        {
            try
            {
                List<Item> items = new List<Item>();
                List<RegionItem> regionItems = _regionItemRepository.GetAllByRegionId(regionId);
                foreach (var regionItem in regionItems)
                {
                    items.Add(_itemRepository.GetById(regionItem.ItemId));
                }

                var itemDtos = items.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
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

        public Result<List<ItemDto>> GetAllNotOnRegion(long regionId)
        {
            try
            {
                List<Item> items = new List<Item>();
                List<RegionItem> regionItems = _regionItemRepository.GetAllByRegionId(regionId);
                foreach (var regionItem in regionItems)
                {
                    items.Add(_itemRepository.GetById(regionItem.ItemId));
                }

                List<Item> itemsReturn = new List<Item>();
                List<Item> allItems = _itemRepository.GetAll();

                foreach (var item in allItems)
                {
                    int flag = 0;
                    foreach (var regionItem in regionItems)
                    {
                        if (regionItem.ItemId == item.Id)
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


        public bool DeleteRegionItem(long regionItemId)
        {
            var regionItemToDelete = _regionItemRepository.Delete(regionItemId);
            return regionItemToDelete != null;
        }

        public bool DeleteByRegionIdAndItemId(long regionId, long itemId)
        {
            var regionItemToDelete = _regionItemRepository.DeleteByRegionIdAndItemId(regionId, itemId);
            return regionItemToDelete != null;
        }
    }
}
