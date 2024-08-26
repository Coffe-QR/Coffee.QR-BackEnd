using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coffee.QR.Core.Services
{
    public class RentOfferService : IRentOfferService
    {
        private readonly IRentOfferRepository _rentOfferRepository;
        private readonly IEmailSender _emailSender;

        public RentOfferService(IRentOfferRepository rentOfferRepository, IEmailSender emailSender)
        {
            _rentOfferRepository = rentOfferRepository;
            _emailSender = emailSender;
        }

        public Result<RentOfferDto> CreateRentOffer(RentOfferDto rentOfferDto)
        {
            try
            {
                var rentOfferStatus = (RentOfferStatus)Enum.Parse(typeof(RentOfferStatus), rentOfferDto.RentOfferStatus.ToString(), true);

                var rentOffer = new RentOffer(
                    rentOfferDto.UserId,
                    rentOfferDto.LocalId,
                    rentOfferDto.Price,
                    rentOfferDto.DateTime,
                    rentOfferStatus
                );

                var createdRentOffer = _rentOfferRepository.Create(rentOffer);

                var resultDto = new RentOfferDto
                {
                    Id = createdRentOffer.Id,
                    UserId = createdRentOffer.UserId,
                    LocalId = createdRentOffer.LocalId,
                    Price = createdRentOffer.Price,
                    DateTime = createdRentOffer.DateTime,
                    RentOfferStatus = createdRentOffer.RentOfferStatus.ToString() // Convert enum back to string
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException ex)
            {
                return Result.Fail<RentOfferDto>("Failed to create RentOffer").WithError(ex.Message);
            }
        }

        public RentOfferDto GetRentOfferById(long id)
        {
            var rentOffer = _rentOfferRepository.GetById(id);
            if (rentOffer == null)
            {
                return null;
            }

            return new RentOfferDto
            {
                Id = rentOffer.Id,
                UserId = rentOffer.UserId,
                LocalId = rentOffer.LocalId,
                Price = rentOffer.Price,
                DateTime = rentOffer.DateTime,
                RentOfferStatus = rentOffer.RentOfferStatus.ToString() // Convert enum back to string
            };
        }

        public List<RentOfferDto> GetAllRentOffers()
        {
            return _rentOfferRepository.GetAll()
                .Select(ro => new RentOfferDto
                {
                    Id = ro.Id,
                    UserId = ro.UserId,
                    LocalId = ro.LocalId,
                    Price = ro.Price,
                    DateTime = ro.DateTime,
                    RentOfferStatus = ro.RentOfferStatus.ToString() // Convert enum back to string
                })
                .ToList();
        }

        public Result<RentOfferDto> UpdateRentOffer(long id, RentOfferDto rentOfferDto)
        {
            try
            {
                var rentOffer = _rentOfferRepository.GetById(id);
                if (rentOffer == null)
                {
                    return Result.Fail<RentOfferDto>("RentOffer not found");
                }

                var rentOfferStatus = (RentOfferStatus)Enum.Parse(typeof(RentOfferStatus), rentOfferDto.RentOfferStatus.ToString(), true);

                rentOffer.UserId = rentOfferDto.UserId;
                rentOffer.LocalId = rentOfferDto.LocalId;
                rentOffer.Price = rentOfferDto.Price;
                rentOffer.DateTime = rentOfferDto.DateTime;
                rentOffer.RentOfferStatus = rentOfferStatus;

                var updatedRentOffer = _rentOfferRepository.Update(rentOffer);

                var resultDto = new RentOfferDto
                {
                    Id = updatedRentOffer.Id,
                    UserId = updatedRentOffer.UserId,
                    LocalId = updatedRentOffer.LocalId,
                    Price = updatedRentOffer.Price,
                    DateTime = updatedRentOffer.DateTime,
                    RentOfferStatus = updatedRentOffer.RentOfferStatus.ToString() // Convert enum back to string
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException ex)
            {
                return Result.Fail<RentOfferDto>("Failed to update RentOffer").WithError(ex.Message);
            }
        }

        public Result DeleteRentOffer(long id)
        {
            try
            {
                bool success = _rentOfferRepository.Delete(id);
                return success ? Result.Ok() : Result.Fail("RentOffer not found");
            }
            catch (Exception ex)
            {
                return Result.Fail("Failed to delete RentOffer").WithError(ex.Message);
            }
        }

        public Result<RentOfferDto> ChangeRentOfferStatus(long id, string status)
        {
            try
            {
                var rentOffer = _rentOfferRepository.GetById(id);
                if (rentOffer == null)
                {
                    return Result.Fail<RentOfferDto>("RentOffer not found");
                }

                var rentOfferStatus = (RentOfferStatus)Enum.Parse(typeof(RentOfferStatus), status, true);
                rentOffer.RentOfferStatus = rentOfferStatus;

                var updatedRentOffer = _rentOfferRepository.Update(rentOffer);

                var resultDto = new RentOfferDto
                {
                    Id = updatedRentOffer.Id,
                    UserId = updatedRentOffer.UserId,
                    LocalId = updatedRentOffer.LocalId,
                    Price = updatedRentOffer.Price,
                    DateTime = updatedRentOffer.DateTime,
                    RentOfferStatus = updatedRentOffer.RentOfferStatus.ToString() // Convert enum back to string
                };

                _emailSender.SendEmail(rentOffer.User.Email, "Rent Offer Review", "Your rent offer for the " + rentOffer.DateTime + " has been " + status );

                return Result.Ok(resultDto);
            }
            catch (ArgumentException ex)
            {
                return Result.Fail<RentOfferDto>("Failed to change RentOffer status").WithError(ex.Message);
            }
        }

        public List<RentOfferDto> GetRentOffersByLocalId(long localId)
        {
            var rentOffers = _rentOfferRepository.GetByLocalId(localId);
            return rentOffers.Select(ro => new RentOfferDto
            {
                Id = ro.Id,
                UserId = ro.UserId,
                LocalId = ro.LocalId,
                Price = ro.Price,
                DateTime = ro.DateTime,
                RentOfferStatus = ro.RentOfferStatus.ToString()
            }).ToList();
        }
    }
}
