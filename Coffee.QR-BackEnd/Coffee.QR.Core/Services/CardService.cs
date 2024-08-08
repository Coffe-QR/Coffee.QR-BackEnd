using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICardEventRepository _cardEventRepository;
        public CardService(ICrudRepository<Card> crudRepository, IMapper mapper, ICardRepository cardRepository, ICardEventRepository cardEventRepository) : base(crudRepository, mapper)
        {
            _cardRepository = cardRepository;
            _cardEventRepository = cardEventRepository;
        }

        public Result<CardDto> CreateCard(CardDto cardDto)
        {
            try
            {
                var card = _cardRepository.Create(new Card( cardDto.Type, cardDto.Note, cardDto.LocalId));

                CardDto resultDto = new CardDto
                {
                    Id = card.Id,
                    Type = cardDto.Type,
                    Note = cardDto.Note,
                    LocalId = cardDto.LocalId
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
                    Type = c.Type,
                    Note = c.Note,
                    LocalId = c.LocalId
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
                    Type = c.Type,
                    Note = c.Note,
                    LocalId = c.LocalId
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
                Type = card.Type,
                Note = card.Note,
                LocalId = card.LocalId
            };
        }

        public async Task<Result> UpdateCardDetails(long cardId, string newType, string newNote, double newPrice)
        {
            try
            {
                var card = await _cardRepository.GetByIdAsync(cardId);
                if (card != null)
                {
                    card.UpdateType(newType);
                    card.UpdateNote(newNote);
                    _cardRepository.UpdateCard(card);
                }

                var cardEvent = await _cardEventRepository.GetCardEventByCardIdAsync(cardId);
                if (cardEvent != null)
                {
                    cardEvent.UpdatePrice(newPrice);
                    _cardEventRepository.UpdateCardEvent(cardEvent);
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error(ex.Message));
            }
        }

        public IEnumerable<CardDto> GetByType(string type)
        {
            var cards = _cardRepository.GetByType(type);
            return cards.Select(c => new CardDto
            {
                Id = c.Id,
                Type = c.Type,
                Note = c.Note,
                LocalId = c.LocalId
            }).ToList();
        }

        public IEnumerable<CardDto> GetByTypeAndEventId(string type, long eventId)
        {
            var cards = _cardRepository.GetByTypeAndEventId(type, eventId);
            return cards.Select(c => new CardDto
            {
                Id = c.Id,
                Type = c.Type,
                Note = c.Note,
                LocalId = c.LocalId
            }).ToList();
        }

    }
}
