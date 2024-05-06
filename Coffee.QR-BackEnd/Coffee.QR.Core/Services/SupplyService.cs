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
    public class SupplyService : CrudService<SupplyDto, Supply>, ISupplyService
    {
        private readonly ISupplyRepository _supplyRepository;


        public SupplyService(ICrudRepository<Supply> crudRepository, IMapper mapper, ISupplyRepository supplyRepository)
            : base(crudRepository, mapper)
        {
            _supplyRepository = supplyRepository;
        }

        public Result<SupplyDto> CreateSupply(SupplyDto supplyDto)
        {
            try
            {
                var supplyt = _supplyRepository.Create(new Supply(supplyDto.CompanyId, supplyDto.TotalPrice, (SupplyStatus)Enum.Parse(typeof(SupplyStatus), supplyDto.Status.ToString(), true)));

                SupplyDto resultDto = new SupplyDto
                {
                    Id = supplyt.Id,
                    CompanyId = supplyt.CompanyId,
                    TotalPrice = supplyt.TotalPrice,
                    Status = (SupplyStatusDto)Enum.Parse(typeof(SupplyStatusDto), supplyDto.Status.ToString(), true),
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<SupplyDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<SupplyDto>> GetAllSupplys()
        {
            try
            {
                var supplys = _supplyRepository.GetAll();
                var supplyDtos = supplys.Select(s => new SupplyDto
                {
                    Id = s.Id,
                    CompanyId = s.CompanyId,
                    TotalPrice = s.TotalPrice,
                    Status = (SupplyStatusDto)Enum.Parse(typeof(SupplyStatusDto), s.Status.ToString(), true),
                }).ToList();

                return Result.Ok(supplyDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<SupplyDto>>("Failed to retrieve supplys").WithError(e.Message);
            }
        }


        public bool DeleteSupply(long supplyId)
        {
            var supplyToDelete = _supplyRepository.Delete(supplyId);
            return supplyToDelete != null;
        }


        public Task<Result<SupplyDto>> GetSupplyByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SupplyDto>> UpdateSupplyAsync(SupplyDto supplyDto)
        {
            throw new NotImplementedException();
        }

        public Result<SupplyDto> GetById(long supplyId)
        {
            try
            {
                var supply = _supplyRepository.GetById(supplyId);
                SupplyDto supplyDtos = new();
                supplyDtos.Id = supply.Id;
                supplyDtos.CompanyId = supply.CompanyId;
                supplyDtos.TotalPrice = supply.TotalPrice;
                supplyDtos.Status = (SupplyStatusDto)Enum.Parse(typeof(SupplyStatusDto), supply.Status.ToString(), true);

                return Result.Ok(supplyDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<SupplyDto>("Failed to retrieve supplys").WithError(e.Message);
            }
        }
    }
}
