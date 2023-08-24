using GameCatalogCore.DTO_s.GenreDTO_s;

namespace GameCatalogDomain.IServices;

public interface IGenreServices
{
    Task<GetGenreByNameDtoResponse> GetGenreByName(string name);
    Task<bool> AddGenre(AddGenreDto request);
}