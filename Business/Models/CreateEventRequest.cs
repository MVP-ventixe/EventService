namespace Business.Models;

public class CreateEventRequest
{
    public string? Image { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}
