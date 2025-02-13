using System;
using API.DTOs.Card;

namespace API.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<CardDto>> GetAllCards();
    Task<CardDto?> GetCardById(int id);
    Task<CardDto> CreateCard(CreateCardDto createCardDto);
    Task<bool> UpdateCard(int id, UpdateCardDto updateCardDto);
    Task<bool> DeleteCard(int id);
}
