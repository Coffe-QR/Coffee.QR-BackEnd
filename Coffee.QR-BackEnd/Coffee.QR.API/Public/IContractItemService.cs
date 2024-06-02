using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IContractItemService
    {
        Result<ContractItemDto> CreateContractItem(ContractItemDto contractItemDto);
        Result<List<ContractItemDto>> GetAllContractItems();
        bool DeleteContractItem(long contractItemId);
        Result<List<ContractItemDto>> CreateContractItems(List<ContractItemDto> contractItemDtos);
    }
}
