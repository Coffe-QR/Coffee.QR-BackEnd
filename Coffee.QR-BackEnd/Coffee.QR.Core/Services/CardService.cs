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
    public class CardService : CrudService<CardDto, Card>, ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICrudRepository<Card> crudRepository, IMapper mapper, ICardRepository cardRepository) : base(crudRepository, mapper)
        {
            _cardRepository = cardRepository;
        }

        public Result<CardDto> CreateCard(CardDto cardDto)
        {
            try
            {
                var card = _cardRepository.Create(new Card(cardDto.Price, cardDto.Type, cardDto.Note, cardDto.EventId));

                CardDto resultDto = new CardDto
                {
                    Price = cardDto.Price,
                    Type = cardDto.Type,
                    Note = cardDto.Note,
                    EventId = cardDto.EventId
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<CardDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public bool DeleteCard(long cardId)
        {
            var cardToDelete = _cardRepository.Delete(cardId);
            return cardToDelete != null;
        }

        public Result<List<CardDto>> GetAllByEventId(long eventId)
        {
            try
            {
                var cards = _cardRepository.GetAllByEventId(eventId);
                var cardDtos = cards.Select(c => new CardDto
                {
                    Id = c.Id,
                    Price = c.Price,
                    Type = c.Type,
                    Note = c.Note,
                    EventId = c.EventId
                }).ToList();

                return Result.Ok(cardDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<CardDto>>("Failed to retrieve cards").WithError(e.Message);
            }
        }

        public Result<List<CardDto>> GetAllCards()
        {
            try
            {
                var cards = _cardRepository.GetAll();
                var cardDtos = cards.Select(c => new CardDto
                {
                    Id = c.Id,
                    Price = c.Price,
                    Type = c.Type,
                    Note = c.Note,
                    EventId = c.EventId
                }).ToList();

                return Result.Ok(cardDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<CardDto>>("Failed to retrieve cards").WithError(e.Message);
            }
        }

        public async Task<CardDto> GetByIdAsync(long id)
        {
            var card = await _cardRepository.GetByIdAsync(id);
            if (card == null)
                return null;

            return new CardDto
            {
                Id = card.Id,
                Price = card.Price,
                Type = card.Type,
                Note = card.Note,
                EventId = card.EventId
            };
        }
    }
}
