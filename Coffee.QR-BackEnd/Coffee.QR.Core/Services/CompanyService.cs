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
    public  class CompanyService : CrudService<CompanyDto, Company>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;


        public CompanyService(ICrudRepository<Company> crudRepository, IMapper mapper, ICompanyRepository companyRepository)
            : base(crudRepository, mapper)
        {
            _companyRepository = companyRepository;
        }

        public Result<CompanyDto> CreateCompany(CompanyDto companyDto)
        {
            try
            {
                var company = _companyRepository.Create(new Company(companyDto.Name));

                CompanyDto resultDto = new CompanyDto
                {
                    Name = company.Name,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<CompanyDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<CompanyDto>> GetAllCompanys()
        {
            try
            {
                var companys = _companyRepository.GetAll();
                var companyDtos = companys.Select(c => new CompanyDto
                {
                    Name = c.Name,
                }).ToList();

                return Result.Ok(companyDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<CompanyDto>>("Failed to retrieve companys").WithError(e.Message);
            }
        }


        public bool DeleteCompany(long companyId)
        {
            var companyToDelete = _companyRepository.Delete(companyId);
            return companyToDelete != null;
        }


        public Task<Result<CompanyDto>> GetCompanyByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CompanyDto>> UpdateCompanyAsync(CompanyDto companyDto)
        {
            throw new NotImplementedException();
        }

    }
}
