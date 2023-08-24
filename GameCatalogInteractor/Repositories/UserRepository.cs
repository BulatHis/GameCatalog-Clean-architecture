using GameCatalogDomain.Entity;
using GameCatalogMsSQL.IRepositories;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8603


namespace GameCatalogInteractor.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AddDbContext _context;

    public UserRepository(AddDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> Check(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CheckByRefreshToken(string refreshToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }


    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AddRefreshToken(User user)
    {
        // Обновляем поля пользователя
        _context.Entry(user).Property(u => u.RefreshToken).IsModified = true;
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> Update(User user)
    {
        _context.Users.Update(user);
        // Сохраняем изменения в базе данных
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> ConfirmUser(User user)
    {
        user.IsConfirmed = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
    
    
}
