using System;
using API.DTOs.Card;
using API.DTOs.Todo;
using API.Entities;
using API.Repository;
using API.Repository.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class CardService : ICardService
{
    private ICardRepository _cardRepository;
    private ITodoRepository _todoRepository;

    public CardService(
        ICardRepository cardRepository,
        ITodoRepository todoRepository)
    {
        _cardRepository = cardRepository;
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<CardDto>> GetAllCards()
    {
        var cards = await _cardRepository.GetAllCards();
        var cardDtos = cards.Select(c => new CardDto()
        {
            Id = c.Id,
            Title = c.Title,
            Todos = c.Todos.Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                CardId = t.CardId
            })
        });

        return cardDtos;
    }

    public async Task<CardDto?> GetCardById(int id)
    {
        // 先取得 Card
        var card = await _cardRepository.GetCardById(id);
        if (card == null) return null;

        // 再取得該 Card 底下所有 Todo
        var todos = await _todoRepository.GetTodosByCardId(id);

        var cardDto = new CardDto()
        {
            Id = card.Id,
            Title = card.Title,
            Todos = todos.Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                CardId = t.CardId
            })
        };

        return cardDto;
    }

    public async Task<CardDto> CreateCard(CreateCardDto createCardDto)
    {
        var card = new Card()
        {
            Title = createCardDto.Title
        };

        await _cardRepository.CreateCard(card);
        await _cardRepository.SaveChangesAsync();

        var cardDto = new CardDto()
        {
            Id = card.Id,
            Title = card.Title
        };
        return cardDto;
    }

    public async Task<bool> UpdateCard(int id, UpdateCardDto updateCardDto)
    {
        var card = await _cardRepository.GetCardById(id);
        if (card == null) return false;

        card.Title = updateCardDto.Title;
        _cardRepository.UpdateCard(card);

        return await _cardRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteCard(int id)
    {
        var card = await _cardRepository.GetCardById(id);
        if (card == null) return false;

        // 取得該卡片下所有 Todo
        var todos = await _todoRepository.GetTodosByCardId(id);

        if (todos.Any())
        {
            _todoRepository.DeleteTodos(todos);
        }

        // 刪除卡片本身
        _cardRepository.DeleteCard(card);

        return await _cardRepository.SaveChangesAsync();
    }
}
