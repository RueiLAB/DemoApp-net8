using API.Data;
using API.Entities;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class CardRepository : ICardRepository
{
    private DataContext _context;

    public CardRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Card>> GetAllCards()
    {
        return await _context.Cards
            .Include(x => x.Todos)
            .ToListAsync();
    }

    public async Task<Card?> GetCardById(int id)
    {
        return await _context.Cards.FindAsync(id);
    }

    public async Task CreateCard(Card card)
    {
        await _context.Cards.AddAsync(card);
    }

    public void UpdateCard(Card card)
    {
        _context.Cards.Update(card);
    }

    public void DeleteCard(Card card)
    {
        _context.Cards.Remove(card);
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
