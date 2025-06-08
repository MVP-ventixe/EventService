using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data;

public class EventPackageEntity
{
    [ForeignKey(nameof(Event))]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public EventEntity Event { get; set; } = null!;

    [ForeignKey(nameof(Package))]

    public string PackageId { get; set; } = null!;

    public PackageEntity Package { get; set; } = null!;
}
