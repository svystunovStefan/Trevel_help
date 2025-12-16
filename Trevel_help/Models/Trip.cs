namespace Trevel_help.Models;

public class Trip
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public List<Place> Places { get; set; } = new();
}
