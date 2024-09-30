using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IErrorSupplyService
    {
        Result<ErrorSupplyDto> Create(ErrorSupplyDto errorSupply);
        Result<List<ErrorSupplyDto>> GetAllForSupply(long supplyId);
        Result<List<ErrorSupplyDto>> CreateList(List<ErrorSupplyDto> supplyItemDtos);
    }
}
