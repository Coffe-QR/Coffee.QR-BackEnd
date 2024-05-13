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
    public class StorageItemService : CrudService<StorageItemDto,StorageItem>, IStorageItemService
    {
        private readonly IStorageItemRepository _storageItemRepository;


        public StorageItemService(ICrudRepository<StorageItem> crudRepository, IMapper mapper, IStorageItemRepository storageItemRepository)
            : base(crudRepository, mapper)
        {
            _storageItemRepository = storageItemRepository;
        }

        public Result<StorageItemDto> CreateStorageItem(StorageItemDto storageItemDto)
        {
            try
            {
                var storageItemt = _storageItemRepository.Create(new StorageItem(storageItemDto.StorageId, storageItemDto.ItemId, storageItemDto.Quantity));

                StorageItemDto resultDto = new StorageItemDto
                {
                    Id = storageItemt.Id,
                    StorageId = storageItemt.StorageId,
                    ItemId = storageItemt.ItemId,
                    Quantity = storageItemt.Quantity,

                    
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<StorageItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<StorageItemDto>> GetAllStorageItems()
        {
            try
            {
                var storageItems = _storageItemRepository.GetAll();
                var storageItemDtos = storageItems.Select(s => new StorageItemDto
                {
                    Id = s.Id,
                    StorageId = s.StorageId,
                    ItemId = s.ItemId,
                    Quantity = s.Quantity,
                }).ToList();

                return Result.Ok(storageItemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<StorageItemDto>>("Failed to retrieve storageItems").WithError(e.Message);
            }
        }


        public bool DeleteStorageItem(long storageItemId)
        {
            var storageItemToDelete = _storageItemRepository.Delete(storageItemId);
            return storageItemToDelete != null;
        }


        public Task<Result<StorageItemDto>> GetStorageItemByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<StorageItemDto>> UpdateStorageItemAsync(StorageItemDto storageItemDto)
        {
            throw new NotImplementedException();
        }
    }
}
