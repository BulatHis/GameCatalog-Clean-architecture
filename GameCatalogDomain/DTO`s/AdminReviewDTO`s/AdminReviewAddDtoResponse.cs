namespace GameCatalogCore.DTO_s.AdminReviewDTO_s;

public class AdminReviewAddDtoResponse
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }

    public string Summary { get; set; } = null!;

    public string GamePlay { get; set; } = null!;

    public string Addictiveness { get; set; } = null!;

    public string Stylization { get; set; } = null!;

    public string Replayable { get; set; } = null!;
    public double Rating { get; set; }

    public int GamePlayRating { get; set; }

    public int AddictivenessRating { get; set; }

    public int StylizationRating { get; set; }

    public int ReplayableRating { get; set; }
    public string Date { get; set; } = null!;
    public string AdminName { get; set; } = null!;
}