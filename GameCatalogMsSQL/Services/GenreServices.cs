using GameCatalogCore.DTO_s.GenreDTO_s;
using GameCatalogDomain.Entity;
using GameCatalogDomain.IServices;
using GameCatalogMsSQL.IRepositories;

namespace GameCatalogMsSQL.Services;

public class GenreServices : IGenreServices
{
    
    private readonly IGenreRepository _genreRepository;

    public GenreServices(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    public async Task<GetGenreByNameDtoResponse> GetGenreByName(string name)
    {
        var genre = await _genreRepository.GetByTitle(name);
        if (genre == null)
        {
            throw new ArgumentException($" id {name} не существует");
        }

        return new GetGenreByNameDtoResponse()
        {
            Id = genre.Id,
            Title = genre.Title,
            Description = genre.Description
        };
    }

    public async Task<bool> AddGenre(AddGenreDto request)
    {
        var genre = new Genre()
        {
            Id = Guid.NewGuid(), Title = request.Title, Description = request.Description
        };
        await _genreRepository.Add(genre);
        return true;
    }
}