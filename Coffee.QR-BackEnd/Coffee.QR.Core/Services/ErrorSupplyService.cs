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

        public ErrorSupplyService(ICrudRepository<Supply> crudRepository, IMapper mapper, IErrorSupplyRepository errorSupplyRepository, ICompanyRepository companyRepository)
            : base(mapper)
        {
            _errorSupplyRepository = errorSupplyRepository;
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
