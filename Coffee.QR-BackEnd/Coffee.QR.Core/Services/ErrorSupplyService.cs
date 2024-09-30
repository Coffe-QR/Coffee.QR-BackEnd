using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace Coffee.QR.Core.Services
{
    public class ErrorSupplyService : BaseService<ErrorSupplyDto, Domain.ErrorSupply>, IErrorSupplyService
    {
        private readonly IErrorSupplyRepository _errorSupplyRepository;
        private readonly ISupplyItemRepository _supplyItemRepository;
        private readonly ISupplyRepository _supplyRepository;


        public ErrorSupplyService(ICrudRepository<Supply> crudRepository, IMapper mapper, IErrorSupplyRepository errorSupplyRepository, ICompanyRepository companyRepository, ISupplyItemRepository supplyItemRepository, ISupplyRepository supplyRepository)
            : base(mapper)
        {
            _errorSupplyRepository = errorSupplyRepository;
            _supplyItemRepository = supplyItemRepository;
            _supplyRepository = supplyRepository;
        }   

        public Result<ErrorSupplyDto> Create(ErrorSupplyDto errorSupply)
        {
            try
            {
                ErrorSupply errorSupply1 = MapToDomain(errorSupply);
                return MapToDto(_errorSupplyRepository.Create(errorSupply1));
            }
            catch (Exception ex)
            {
                return Result.Fail<ErrorSupplyDto>(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<ErrorSupplyDto>> CreateList(List<ErrorSupplyDto> supplyItemDtos)
        {
            try
            {
                List<ErrorSupply> errorSupplies = MapToDomain(supplyItemDtos);

                List<SupplyItem> supplyItems = _supplyItemRepository.GetAllForSupply(errorSupplies[0].SupplyId);
                foreach(var e in errorSupplies)
                {
                    e.Id = 0;
                    if(supplyItems.Find(i => i.ItemId == e.ItemId) == null)
                    {
                        e.Status = ErrorStatus.MISSTAKE;
                        e.ExpectedQuantity = 0;
                    }
                    else
                    {
                        e.ExpectedQuantity = supplyItems.Find(i => i.ItemId == e.ItemId).Quantity;
                        e.Status = ErrorStatus.DAMAGED;
                    }
                    _errorSupplyRepository.Create(e);
                }
                Supply supply = _supplyRepository.GetById(errorSupplies[0].SupplyId);
                supply.Status = SupplyStatus.MISTAKE;
                _supplyRepository.Save();
             
                return MapToDto(errorSupplies);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<ErrorSupplyDto>>(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<ErrorSupplyDto>> GetAllForSupply(long supplyId)
        {
            try
            {
                return MapToDto(_errorSupplyRepository.GetAllForSupply(supplyId));
            }
            catch (Exception ex)
            {
                return Result.Fail<List<ErrorSupplyDto>>(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }
    }
}
