using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data;

public class PackageEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } =  null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public string? Currency { get; set; }

}