using System.ComponentModel.DataAnnotations;

namespace Spendee.Shared.Models;

public record CategoryDTO
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
    public string Name { get; set; }
}
