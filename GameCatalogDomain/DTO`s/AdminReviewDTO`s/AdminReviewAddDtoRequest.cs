using System.ComponentModel.DataAnnotations;

// ReSharper disable All

namespace GameCatalogCore.DTO_s.AdminReviewDTO_s;

public class AdminReviewAddDtoRequest
{
    [MaxLength(500)] public string GamePlay { get; set; } = null!;
    [MaxLength(500)] public string Addictiveness { get; set; } = null!;
    [MaxLength(500)] public string Stylization { get; set; } = null!;
    [MaxLength(500)] public string Replayable { get; set; } = null!;
    [MaxLength(500)] public string Summary { get; set; } = null!;
     public int GamePlayRating { get; set; }
     public int AddictivenessRating { get; set; }
     public int StylizationRating { get; set; }
     public int ReplayableRating { get; set; }
    public Guid GameId { get; set; }
    public string AdminName { get; set; } = null!;
}