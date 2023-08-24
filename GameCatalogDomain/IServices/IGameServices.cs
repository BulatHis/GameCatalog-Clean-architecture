using GameCatalogCore.DTO_s.GameDTO_s;
using GameCatalogDomain.DTO_s.GameDTO_s;

namespace GameCatalogDomain.IServices;

public interface IGameServices
{
    Task<AllGamesDtoResponse> GetAllGames();
    Task<GetGameByIdResponse> GetGameById(Guid id);
    Task<AllTitlesAndImgDtoResponse> GetAllTitles();
    Task<bool> Delete(Guid id);
    
    Task<bool> Add(AddGameDto request);

    Task<GetGameByIdResponse> CheckByTitle(string name);
}