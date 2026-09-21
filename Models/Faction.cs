using System.ComponentModel.DataAnnotations;

namespace FactionFactory.Api.Models;

public record Faction
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string Motto { get; set; }

    [MaxLength(2000)]
    public string Description { get; set; }

    [MaxLength(50)]
    public string Type { get; set; }

    [MaxLength(50)]
    public string Organisation { get; set; }

    public string? EmblemFileName { get; set; }
}
