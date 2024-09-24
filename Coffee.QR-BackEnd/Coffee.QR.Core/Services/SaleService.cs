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
    public class SaleService : BaseService<SaleDto, Sale>, ISaleService
    {
        protected readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository, IMapper mapper) : base(mapper)
        {
            _saleRepository = saleRepository;
        }

        public Result<List<SaleDto>> GetAllForCompany(string companyName)
        {
            try
            {
                return MapToDto(_saleRepository.GetAllForCompany(companyName));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }
    }
}
