using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ISupplyService
    {
        Result<SupplyDto> CreateSupply(SupplyDto supplyDto);
        Result<List<SupplyDto>> GetAllSupplys();
        bool DeleteSupply(long supplyId);
        Task<Result<SupplyDto>> GetSupplyByIdAsync(long id);
        Task<Result<SupplyDto>> UpdateSupplyAsync(SupplyDto supplyDto);

        Result<SupplyDto> GetById(long supplyId);
    }
}
