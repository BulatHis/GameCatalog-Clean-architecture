using GameCatalogDomain.Entity;
using GameCatalogDomain.IRepositories;
using GameCatalogInteractor;
using GameCatalogMsSQL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogMsSQL.Repositories;

public class GameRepository : IGameRepository
{
    private readonly AddDbContext _context;

    public GameRepository(AddDbContext context)
    {
        _context = context;
    }

    public async Task<List<Game>> GetAll()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task<List<string>> GetTitles()
    {
        return await _context.Games.Select(g => g.Title).ToListAsync();
    }
    
    public async Task<List<string>> GetImages()
    {
        return await _context.Games.Select(g => g.Image).ToListAsync();
    }

    public async Task<Game> Check(Guid id)
    {
        return (await _context.Games.FirstOrDefaultAsync(u => u.GameId == id))!;
    }
    
    public async Task<Game> CheckByTitle(string name)
    {
        return (await _context.Games.FirstOrDefaultAsync(u => u.Title == name))!;
    }

    public async Task Add(Game game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Game game)
    {
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }
}

