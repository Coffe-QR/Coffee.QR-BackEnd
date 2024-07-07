using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class CardEventService : CrudService<CardEventDto, CardEvent>, ICardEventService
    {
        private readonly ICardEventRepository _cardEventRepository;
        public CardEventService(ICrudRepository<CardEvent> crudRepository, IMapper mapper, ICardEventRepository cardEventRepository) : base(crudRepository, mapper)
        {
            _cardEventRepository = cardEventRepository;
        }

        public Result<CardEventDto> CreateCardEvent(CardEventDto cardDto)
        {
            try
            {
                var card = _cardEventRepository.Create(new CardEvent(cardDto.CardId, cardDto.Price, cardDto.EventId));

                CardEventDto resultDto = new CardEventDto
                {
                    CardId = cardDto.CardId,
                    Price = cardDto.Price,
                    EventId = cardDto.EventId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<CardEventDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public bool DeleteCardEvent(long cardEventId)
        {
            var cardEventToDelete = _cardEventRepository.Delete(cardEventId);
            return cardEventToDelete != null;
        }

        public Result<List<CardEventDto>> GetAllByEventId(long eventId)
        {
            try
            {
                var cards = _cardEventRepository.GetAllByEventId(eventId);
                var cardEventDtos = cards.Select(c => new CardEventDto
                {
                    Id = c.Id,
                    CardId = c.CardId,
                    Price = c.Price,
                    EventId = c.EventId,
                }).ToList();

                return Result.Ok(cardEventDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<CardEventDto>>("Failed to retrieve cards").WithError(e.Message);
            }
        }

        public Result<List<CardEventDto>> GetAllCardEvents()
        {
            try
            {
                var cards = _cardEventRepository.GetAll();
                var cardEventDtos = cards.Select(c => new CardEventDto
                {
                    Id = c.Id,
                    CardId = c.CardId,
                    Price = c.Price,
                    EventId = c.EventId,
                }).ToList();

                return Result.Ok(cardEventDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<CardEventDto>>("Failed to retrieve cards").WithError(e.Message);
            }
        }

        public async Task<CardEventDto> GetByIdAsync(long id)
        {
            var cardevent = await _cardEventRepository.GetByIdAsync(id);
            if (cardevent == null)
                return null;

            return new CardEventDto
            {
                Id = cardevent.Id,
                CardId = cardevent.CardId,
                Price = cardevent.Price,
                EventId = cardevent.EventId,
            };
        }

    }
}
