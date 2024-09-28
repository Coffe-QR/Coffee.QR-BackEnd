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
using System.Diagnostics.Contracts;

namespace Coffee.QR.Core.Services
{
    public class ContractService : CrudService<ContractDto, Domain.Contract>, IContractService
    {
        private readonly IContractRepository _contractRepository;


        public ContractService(ICrudRepository<Domain.Contract> crudRepository, IMapper mapper, IContractRepository contractRepository)
            : base(crudRepository, mapper)
        {
            _contractRepository = contractRepository;
        }
        public Result<ContractDto> CreateContract(ContractDto contractDto)
        {
            try
            {
                var contractt = _contractRepository.Create(new Domain.Contract(contractDto.LocalId, contractDto.CompanyId, contractDto.Description, contractDto.Start,  contractDto.SupplyId));

                ContractDto resultDto = new ContractDto
                {
                    Id = contractt.Id,
                    Start = contractt.Start,
                    Frequency = (FrequencyDto)Enum.Parse(typeof(FrequencyDto), contractt.Frequency.ToString(), true),
                    CompanyId = contractt.CompanyId,
                    Description = contractt.Description,
                    LocalId = contractt.LocalId,
                    SupplyId = contractt.SupplyId
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ContractDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<ContractDto>> GetAllContracts()
        {
            try
            {
                var contracts = _contractRepository.GetAll();
                var contractDtos = contracts.Select(contractt => new ContractDto
                {
                    Id = contractt.Id,
                    Start = contractt.Start,
                    Frequency = (FrequencyDto)Enum.Parse(typeof(FrequencyDto), contractt.Frequency.ToString(), true),
                    CompanyId = contractt.CompanyId,
                    Description = contractt.Description,
                    LocalId = contractt.LocalId,
                    SupplyId = contractt.SupplyId
                }).ToList();

                return Result.Ok(contractDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ContractDto>>("Failed to retrieve contracts").WithError(e.Message);
            }
        }
        public bool DeleteContract(long contractId)
        {
            var contractToDelete = _contractRepository.Delete(contractId);
            return contractToDelete != null;
        }
        public Result<List<ContractDto>> GetAllForLocal(long localId)
        {
            try
            {
                var contracts = _contractRepository.GetAll().FindAll(c => c.LocalId == localId);
                var contractDtos = contracts.Select(contractt => new ContractDto
                {
                    Id = contractt.Id,
                    Start = contractt.Start,
                    Frequency = (FrequencyDto)Enum.Parse(typeof(FrequencyDto), contractt.Frequency.ToString(), true),
                    CompanyId = contractt.CompanyId,
                    Description = contractt.Description,
                    LocalId = contractt.LocalId,
                    SupplyId = contractt.SupplyId
                }).ToList();

                return Result.Ok(contractDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ContractDto>>("Failed to retrieve contracts").WithError(e.Message);
            }
        }
    }
}
