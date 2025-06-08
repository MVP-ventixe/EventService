namespace Infrastructure.Models;

public enum ResponseStatus
{
    Success,
    NotFound,
    Found,
    Invalid,
    Error
}

public class EventResult
{
    public bool IsSuccess { get; set; }
    public ResponseStatus? Status { get; set; }
    public string? ErrorMessage { get; set; }


}
public class EventResult<T> : EventResult
{
    public T? Result { get; set; }
}
