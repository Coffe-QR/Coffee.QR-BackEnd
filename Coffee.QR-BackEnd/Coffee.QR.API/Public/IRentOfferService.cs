using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IRentOfferService
    {
        Result<RentOfferDto> CreateRentOffer(RentOfferDto rentOfferDto);
        RentOfferDto GetRentOfferById(long id);
        List<RentOfferDto> GetAllRentOffers();
        Result<RentOfferDto> UpdateRentOffer(long id, RentOfferDto rentOfferDto);
        Result DeleteRentOffer(long id);
        Result<RentOfferDto> ChangeRentOfferStatus(long id, string status);
        List<RentOfferDto> GetRentOffersByLocalId(long localId);


    }
}
