using GameCatalogDomain.Entity;

namespace GameCatalogDomain.IRepositories;

public interface IGameRepository
{
    Task<List<Game>> GetAll();
    Task<List<string>> GetTitles();
    Task<List<string>> GetImages();
    Task<Game> Check(Guid id);
    Task Add(Game game);
    Task Delete(Game game);
    Task<Game> CheckByTitle(string name);
}