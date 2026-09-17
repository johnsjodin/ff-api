namespace FactionFactory.Api.Models;

public record Faction
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Motto { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Organisation { get; set; }
}
