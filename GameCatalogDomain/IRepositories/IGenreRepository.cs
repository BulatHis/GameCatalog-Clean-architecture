using GameCatalogDomain.Entity;

namespace GameCatalogMsSQL.IRepositories;

public interface IGenreRepository
{
    Task<Genre> GetByTitle(string name);
    Task Add(Genre genre);
    /*Task Delete(Guid id);*/
}