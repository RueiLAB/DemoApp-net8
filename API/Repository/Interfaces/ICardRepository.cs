using System;
using API.Entities;

namespace API.Repository.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<Card>> GetAllCards();
    Task<Card?> GetCardById(int id);
    Task CreateCard(Card card);
    void UpdateCard(Card card);
    void DeleteCard(Card card);
    Task<bool> SaveChangesAsync();
}
