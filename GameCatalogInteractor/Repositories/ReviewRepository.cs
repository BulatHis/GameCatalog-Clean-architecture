using GameCatalogDomain.Entity;
using GameCatalogDomain.IRepositories;
using GameCatalogInteractor;
using GameCatalogMsSQL.IRepositories;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8603

namespace GameCatalogMsSQL.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AddDbContext _context;

    public ReviewRepository(AddDbContext context)
    {
        _context = context;
    }

    public async Task<Review> Check(Guid id)
    {
        return await _context.Reviews.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> Add(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<List<Review>> GetByGameId(Guid gameId)
    {
        return await _context.Reviews.Where(r => r.GameId == gameId).ToListAsync();
    }


    public async Task<bool> Delete(Review review)
    {
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }
}

