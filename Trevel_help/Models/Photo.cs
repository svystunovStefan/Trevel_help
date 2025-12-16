namespace Trevel_help.Models;

public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public int PlaceId { get; set; }
    public Place Place { get; set; } = null!;
}

