namespace Trevel_help.Models;

public class Place
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    public List<Photo> Photos { get; set; } = new();
}
