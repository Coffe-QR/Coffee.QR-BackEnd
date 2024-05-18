using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class ItemService : CrudService<ItemDto, Item>, IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IStorageItemRepository _storageItemRepository;
        public ItemService(ICrudRepository<Item> crudRepository, IMapper mapper, IItemRepository itemRepository, IStorageItemRepository storageItemRepository)
            : base(crudRepository, mapper)
        {
            _itemRepository = itemRepository;
            _storageItemRepository = storageItemRepository;
        }

        public Result<ItemDto> CreateItem(ItemDto itemDto)
        {
            try
            {
                var item = _itemRepository.Create(new Item((ItemType)Enum.Parse(typeof(ItemType), itemDto.Type.ToString(), true), itemDto.Name, itemDto.Description, itemDto.Price, itemDto.Picture));

                ItemDto resultDto = new ItemDto
                {
                    Id = item.Id,
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), itemDto.Type.ToString(), true),
                    Price = itemDto.Price,
                    Picture = itemDto.Picture,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public bool DeleteItem(long itemId)
        {
            var itemToDelete = _itemRepository.Delete(itemId);
            return itemToDelete != null;                        
        }

        public Result<List<ItemDto>> GetAllItems()
        {
            try
            {
                var items = _itemRepository.GetAll();
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
                return Result.Fail<List<ItemDto>>("Failed to retrieve events").WithError(e.Message);
            }
        }

  

        public Result<List<ItemDto>> GetAllForStorage(long storageId)
        {
            try
            {
                List<ItemDto> dtos = new();
                foreach (var si in _storageItemRepository.GetAll().FindAll(s => s.StorageId == storageId))
                {
                    Item item = _itemRepository.GetItem(si.Id);
                    ItemDto dto = new ItemDto()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), item.Type.ToString(), true),
                        Price = item.Price,
                        Picture = item.Picture,
                        Quantity = si.Quantity,
                    };
                    dtos.Add(dto);
                }
                return  Result.Ok(dtos);
            }
            catch(Exception e)
            {
                return Result.Fail<List<ItemDto>>("Failed to retrieve events").WithError(e.Message);
            }
        }


        public bool UpdateItem(ItemDto newItem)
        {
            Item oldItem = _itemRepository.GetById(newItem.Id);
            oldItem.Name = newItem.Name;
            oldItem.Description = newItem.Description;
            oldItem.Price = newItem.Price;
            oldItem.Picture = newItem.Picture;
            return _itemRepository.UpdateItem(oldItem);
        }

        public Result<ItemDto> GetById(long itemId)
        {
            try
            {
                Item item = _itemRepository.GetById(itemId);
                if (item != null)
                {
                    ItemDto itemDto = new ItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        Picture = item.Picture,
                        Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), item.Type.ToString(), true),
                    };
                    return Result.Ok(itemDto);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return Result.Fail<ItemDto>("Failed to retrieve items").WithError(e.Message);
            }
        }

    }
}
