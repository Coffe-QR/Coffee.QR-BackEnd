using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class LocalRentPriceListService : CrudService<LocalRentPriceListDto,LocalRentPriceList>,ILocalRentPriceListService
    {
        private readonly ILocalRentPriceListRepository _localRentPriceListRepository;
        public LocalRentPriceListService(ICrudRepository<LocalRentPriceList> crudRepository,IMapper mapper, ILocalRentPriceListRepository localRentPriceListRepository) : base(crudRepository, mapper)
        {
            _localRentPriceListRepository = localRentPriceListRepository;
        }

        public Result<LocalRentPriceListDto> CreateLocalRentPriceList(LocalRentPriceListDto localRentPriceListDto)
        {
            try
            {
                var localRentPriceList = new LocalRentPriceList(
                    localRentPriceListDto.LocalId,
                    localRentPriceListDto.Price,
                    localRentPriceListDto.PricingDate,
                    true 
                );

                var createdLocalRentPriceList = _localRentPriceListRepository.CreateAndDeactivateExisting(localRentPriceList);

                LocalRentPriceListDto resultDto = new LocalRentPriceListDto
                {
                    Id = createdLocalRentPriceList.Id,
                    LocalId = createdLocalRentPriceList.LocalId,
                    Price = createdLocalRentPriceList.Price,
                    PricingDate = createdLocalRentPriceList.PricingDate,
                    IsActive = createdLocalRentPriceList.IsActive
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<LocalRentPriceListDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<LocalRentPriceListDto> GetActiveLocalRentPriceList(long localId)
        {
            var activePriceList = _localRentPriceListRepository.GetActiveByLocalId(localId);

            if (activePriceList == null)
            {
                return Result.Fail<LocalRentPriceListDto>(FailureCode.NotFound).WithError("Active LocalRentPriceList not found.");
            }

            var resultDto = new LocalRentPriceListDto
            {
                Id = activePriceList.Id,
                LocalId = activePriceList.LocalId,
                Price = activePriceList.Price,
                PricingDate = activePriceList.PricingDate,
                IsActive = activePriceList.IsActive
            };

            return Result.Ok(resultDto);
        }



    }
}
