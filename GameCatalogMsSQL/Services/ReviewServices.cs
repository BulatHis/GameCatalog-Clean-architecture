using GameCatalogCore.DTO_s.ReviewsDTO_s;
using GameCatalogCore.IServices;
using GameCatalogDomain.Entity;
using GameCatalogDomain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameCatalogCore.Services;

public class ReviewServices : IReviewServices
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ILogger<ReviewServices> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReviewServices(IReviewRepository reviewRepository, ILogger<ReviewServices> logger , IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _reviewRepository = reviewRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateReviewDtoResponse> AddReview(CreateReviewDtoRequest request)
    {
        var userId = _httpContextAccessor.HttpContext!.User.Claims.First(x => x.Type == "id").Value;
        var review = new Review
        {
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            Id = Guid.NewGuid(), Text = request.Text, Rating = request.Rating, Date = DateTime.Today.ToString("dd/MM/yyyy"),
            UserId = Guid.Parse(userId), GameId = request.GameId
        };
        await _reviewRepository.Add(review);
        return new CreateReviewDtoResponse() { Result = true };
    }

    public async Task<UpdateTextReviewDtoResponse> UpdateReviewText(UpdateTextReviewDtoRequest request)
    {
        var review = await _reviewRepository.Check(request.Id);
        if (review == null)
        {
            throw new ArgumentException($" id {request.Id} не существует");
        }

        review.Text = request.Text;
        await _reviewRepository.Update(review);
        return new UpdateTextReviewDtoResponse() { Result = true };
    }

    public async Task<GetReviewByGameIdDtoResponse> GetReviewByGameId(GetReviewByGameIdDtoRequest request)
    {
        _logger.LogInformation(@"""Вызван метод по получению погоды на 5 дней. 
				Время вызова {TimeToSendRequest}""" , DateTime.Now);
        var rewiew = await _reviewRepository.GetByGameId(request.GameId);
        if (rewiew.Count != 0)
        {
            return new GetReviewByGameIdDtoResponse()
            {
                Id = rewiew.Select(r => r.Id).ToList(),
                Text = rewiew.Select(r => r.Text).ToList(),
                Rating = rewiew.Select(r => r.Rating).ToList(),
                Date = rewiew.Select(r => r.Date).ToList(),
                UserId = rewiew.Select(r => r.UserId).ToList(),
                GameId = rewiew.Select(r => r.GameId).ToList(),
            };
        }

        throw new ArgumentException($"no game id {request.GameId} ");
    }

    public async Task<GetReviewByIdDtoResponse> GetReviewById(GetReviewByIdDtoRequest request)
    {
        var review = await _reviewRepository.Check(request.Id);
        if (review == null)
        {
            throw new ArgumentException($"no game id {request.Id} ");
        }

        return new GetReviewByIdDtoResponse()
        {
            Text = review.Text, Rating = review.Rating, Date = review.Date, UserId = review.UserId,
            GameId = review.GameId
        };
    }

    public async Task<DeleteReviewDtoResponse> DeleteReview(DeleteReviewDtoRequest request)
    {
        var review = await _reviewRepository.Check(request.Id);
        if (review == null)
        {
            throw new ArgumentException($"no Review {request.Id} ");
        }

        await _reviewRepository.Delete(review);
        return new DeleteReviewDtoResponse() { Result = true };
    }
}