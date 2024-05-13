using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ISupplyItemService
    {
        Result<SupplyItemDto> CreateSupplyItem(SupplyItemDto supplyItemDto);
        Result<List<SupplyItemDto>> GetAllSupplyItems();
        bool DeleteSupplyItem(long supplyItemId);
        Task<Result<SupplyItemDto>> GetSupplyItemByIdAsync(long id);
        Task<Result<SupplyItemDto>> UpdateSupplyItemAsync(SupplyItemDto supplyItemDto);

        Result<List<SupplyItemDto>> CreateSupplyItems(List<SupplyItemDto> supplyItemDtos);
        Result<List<SupplyItemDto>> GetAllForSupply(long supplyId);

    }
}
