using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ICompanyService
    {
        Result<CompanyDto> CreateCompany(CompanyDto company);
        Result<List<CompanyDto>> GetAllCompanys();
        bool DeleteCompany(long eventId);
        Task<Result<CompanyDto>> GetCompanyByIdAsync(long id);
        Task<Result<CompanyDto>> UpdateCompanyAsync(CompanyDto company);
    }
}
