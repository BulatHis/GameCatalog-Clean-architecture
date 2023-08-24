using GameCatalogDomain.Entity;
using GameCatalogInteractor;
using GameCatalogMsSQL.IRepositories;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8603

namespace GameCatalogMsSQL.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AddDbContext _context;

    public GenreRepository(AddDbContext context)
    {
        _context = context;
    }

    public async Task<Genre> GetByTitle(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(g =>g.Title ==name);
    }

    public async Task Add(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            throw new ArgumentException("Genre not found");
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }
}

