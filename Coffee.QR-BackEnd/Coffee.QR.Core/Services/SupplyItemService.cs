using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class SupplyItemService : CrudService<SupplyItemDto, SupplyItem>, ISupplyItemService
    {
        private readonly ISupplyItemRepository _supplyItemRepository;


        public SupplyItemService(ICrudRepository<SupplyItem> crudRepository, IMapper mapper, ISupplyItemRepository supplyItemRepository)
            : base(crudRepository, mapper)
        {
            _supplyItemRepository = supplyItemRepository;
        }

        public Result<SupplyItemDto> CreateSupplyItem(SupplyItemDto supplyItemDto)
        {
            try
            {
                var supplyItemt = _supplyItemRepository.Create(new SupplyItem(supplyItemDto.SupplyId, supplyItemDto.ItemId, supplyItemDto.Quantity, supplyItemDto.Price));

                SupplyItemDto resultDto = new SupplyItemDto
                {
                    Id = supplyItemt.Id,
                    Quantity = supplyItemt.Quantity,
                    Price = supplyItemt.Price,
                    SupplyId = supplyItemt.SupplyId,
                    ItemId = supplyItemt.ItemId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<SupplyItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<SupplyItemDto>> GetAllSupplyItems()
        {
            try
            {
                var supplyItems = _supplyItemRepository.GetAll();
                var supplyItemDtos = supplyItems.Select(s => new SupplyItemDto
                {
                    Id = s.Id,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    SupplyId = s.SupplyId,
                    ItemId = s.ItemId,
                }).ToList();

                return Result.Ok(supplyItemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<SupplyItemDto>>("Failed to retrieve supplyItems").WithError(e.Message);
            }
        }


        public bool DeleteSupplyItem(long supplyItemId)
        {
            var supplyItemToDelete = _supplyItemRepository.Delete(supplyItemId);
            return supplyItemToDelete != null;
        }


        public Task<Result<SupplyItemDto>> GetSupplyItemByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SupplyItemDto>> UpdateSupplyItemAsync(SupplyItemDto supplyItemDto)
        {
            throw new NotImplementedException();
        }

        public Result<List<SupplyItemDto>> CreateSupplyItems(List<SupplyItemDto> supplyItemDtos)
        {
            try
            {
                List<SupplyItemDto> resultDtos = new();
                foreach(var supplyItemDto in supplyItemDtos)
                {
                    var supplyItemt = _supplyItemRepository.Create(new SupplyItem(supplyItemDto.SupplyId, supplyItemDto.ItemId, supplyItemDto.Quantity, supplyItemDto.Price));

                    SupplyItemDto resultDto = new SupplyItemDto
                    {
                        Id = supplyItemt.Id,
                        Quantity = supplyItemt.Quantity,
                        Price = supplyItemt.Price,
                        SupplyId = supplyItemt.SupplyId,
                        ItemId = supplyItemt.ItemId,
                    };
                    resultDtos.Add(resultDto);
                }

                return Result.Ok(resultDtos);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<List<SupplyItemDto>>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<SupplyItemDto>> GetAllForSupply(long supplyId)
        {
            try
            {
                List<SupplyItemDto> dtos = new();
                foreach (var item in GetAllSupplyItems().Value)
                {
                    if (item.SupplyId == supplyId) dtos.Add(item);
                }
                return Result.Ok(dtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<SupplyItemDto>>("Failed to retrieve supplyItems").WithError(e.Message);
            }
        }
    }
}
