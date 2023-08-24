using GameCatalogDomain.Entity;
using GameCatalogMsSQL.IRepositories;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8603

namespace GameCatalogInteractor.Repositories;

public class AdminReviewRepository : IAdminReviewRepository
{
    private readonly AddDbContext _context;

    public AdminReviewRepository(AddDbContext context)
    {
        _context = context;
    }

    public async Task<AdminReview> Check(Guid id)
    {
        return (await _context.AdminReview.FirstOrDefaultAsync(u => u.Id == id))!;
    }

    public async Task Add(AdminReview adminReview)
    {
        await _context.AdminReview.AddAsync(adminReview);
        await _context.SaveChangesAsync();
    }

    public async Task<AdminReview> GetByGameId(Guid gameId)
    {
        return await _context.AdminReview.FirstOrDefaultAsync(r => r.GameId == gameId);
    }

    public async Task Delete(AdminReview adminReview)
    {
        _context.AdminReview.Remove(adminReview);
        await _context.SaveChangesAsync();
    }
}

