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
    public class SupplyService : BaseService<SupplyDto, Supply>, ISupplyService
    {
        private readonly ISupplyRepository _supplyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISupplyItemRepository _supplyItemRepository;

        public SupplyService(ICrudRepository<Supply> crudRepository, IMapper mapper, ISupplyRepository supplyRepository, ICompanyRepository companyRepository, ISupplyItemRepository supplyItemRepository)
            : base(mapper)
        {
            _supplyRepository = supplyRepository;
            _companyRepository = companyRepository;
            _supplyItemRepository = supplyItemRepository;
        }

        public Result<SupplyDto> CreateSupply(SupplyDto supplyDto)
        {
            try
            {
                var supply = new Supply(supplyDto.CompanyId, supplyDto.TotalPrice, (SupplyStatus)Enum.Parse(typeof(SupplyStatus), supplyDto.Status.ToString(), true));
                var dateTime = DateTime.UtcNow;
                supply.Ordered = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                var supplyt = _supplyRepository.Create(supply);

                SupplyDto resultDto = new SupplyDto
                {
                    Id = supplyt.Id,
                    CompanyId = supplyt.CompanyId,
                    TotalPrice = supplyt.TotalPrice,
                    Status = (SupplyStatusDto)Enum.Parse(typeof(SupplyStatusDto), supplyDto.Status.ToString(), true),
                    Ordered = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day)
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
                    CompanyName = _companyRepository.Get(s.CompanyId).Name,
                    Ordered = s.Ordered
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

        public Result<SupplyDto> Taken(long supplyId)
        {
            try
            {
                Supply supply = _supplyRepository.GetById(supplyId);
                supply.Taken();
                _supplyRepository.Save();
                return MapToDto(supply);
            }
            catch (Exception e)
            {
                return Result.Fail<SupplyDto>("Failed to retrieve supplys").WithError(e.Message);
            }
        }

        public Result<SupplyDto> Confirm(long supplyId)
        {
            try
            {
                Supply supply = _supplyRepository.GetById(supplyId);
                supply.Confirm();
                _supplyRepository.Save();
                return MapToDto(supply);
            }
            catch (Exception e)
            {
                return Result.Fail<SupplyDto>("Failed to retrieve supplys").WithError(e.Message);
            }
        }

        public Result<SupplyDto> Reorder(long supplyId)
        {
            try
            {
                Supply supply = _supplyRepository.GetById(supplyId);

                supply.Id = 0;
                supply = _supplyRepository.Create(supply);

                List<SupplyItem> supplyItems = _supplyItemRepository.GetAllForSupply(supplyId);
                foreach(var si in supplyItems)
                {
                    SupplyItem supplyItem = new(si, supply.Id);
                    _supplyItemRepository.Create(supplyItem);
                }

                return MapToDto(supply);
            }
            catch (Exception e)
            {
                return Result.Fail<SupplyDto>("Failed to retrieve supplys").WithError(e.Message);
            }
        }
    }
}