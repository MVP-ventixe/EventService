using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Image { get; set; }
    public string? Name { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime Date { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<EventPackageEntity> EventPackages { get; set; } = [];

}
