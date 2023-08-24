using GameCatalogDomain.Entity;

namespace GameCatalogMsSQL.IRepositories;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<User> Check(Guid id);
    Task<User> CheckByRefreshToken(string refreshToken);
    Task Add(User user);
    Task<bool> AddRefreshToken(User user);
    Task<bool> Update(User user);
    Task<bool> ConfirmUser(User user);
    Task<bool> Delete(User user);
}