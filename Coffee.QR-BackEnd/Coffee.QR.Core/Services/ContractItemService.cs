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
    public class ContractItemService : CrudService<ContractItemDto, ContractItem>, IContractItemService
    {
        private readonly IContractItemRepository _contractItemRepository;


        public ContractItemService(ICrudRepository<ContractItem> crudRepository, IMapper mapper, IContractItemRepository contractItemRepository)
            : base(crudRepository, mapper)
        {
            _contractItemRepository = contractItemRepository;
        }

        public Result<ContractItemDto> CreateContractItem(ContractItemDto contractItemDto)
        {
            try
            {
                var contractItemt = _contractItemRepository.Create(new ContractItem(contractItemDto.ContractId, contractItemDto.ItemId, contractItemDto.Quantity, contractItemDto.Price));

                ContractItemDto resultDto = new ContractItemDto
                {
                    Id = contractItemt.Id,
                    Quantity = contractItemt.Quantity,
                    Price = contractItemt.Price,
                    ContractId = contractItemt.ContractId,
                    ItemId = contractItemt.ItemId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ContractItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<ContractItemDto>> GetAllContractItems()
        {
            try
            {
                var contractItems = _contractItemRepository.GetAll();
                var contractItemDtos = contractItems.Select(s => new ContractItemDto
                {
                    Id = s.Id,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    ContractId = s.ContractId,
                    ItemId = s.ItemId,
                }).ToList();

                return Result.Ok(contractItemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ContractItemDto>>("Failed to retrieve contractItems").WithError(e.Message);
            }
        }


        public bool DeleteContractItem(long contractItemId)
        {
            var contractItemToDelete = _contractItemRepository.Delete(contractItemId);
            return contractItemToDelete != null;
        }

        public Result<List<ContractItemDto>> CreateContractItems(List<ContractItemDto> contractItemDtos)
        {
            try
            {
                List<ContractItemDto> resultDtos = new();
                foreach (var contractItemDto in contractItemDtos)
                {
                    var contractItemt = _contractItemRepository.Create(new ContractItem(contractItemDto.ContractId, contractItemDto.ItemId, contractItemDto.Quantity, contractItemDto.Price));

                    ContractItemDto resultDto = new ContractItemDto
                    {
                        Id = contractItemt.Id,
                        Quantity = contractItemt.Quantity,
                        Price = contractItemt.Price,
                        ContractId = contractItemt.ContractId,
                        ItemId = contractItemt.ItemId,
                    };
                    resultDtos.Add(resultDto);
                }

                return Result.Ok(resultDtos);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<List<ContractItemDto>>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


    }
}
