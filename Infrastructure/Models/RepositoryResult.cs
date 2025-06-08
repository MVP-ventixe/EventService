namespace Infrastructure.Models;

public enum ResponseStatus
{
    Success,
    NotFound,
    Found,
    Invalid,
    Error
}

public class RepositoryResult
{
    public bool IsSuccess { get; set; }
    public ResponseStatus? Status { get; set; }
    public string? ErrorMessage { get; set; }


}
    public class RepositoryResult<T> : RepositoryResult
    {
        public T? Result { get; set; }
    }
