using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ILocalRentPriceListService
    {
        Result<LocalRentPriceListDto> CreateLocalRentPriceList(LocalRentPriceListDto localRentPriceListDto);
        Result<LocalRentPriceListDto> GetActiveLocalRentPriceList(long localId);
    }
}
