namespace Infrastructure.Models;

internal class RepositoryResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }


    public class RepositoryResult<T> : RepositoryResult
    {
        public T? Result { get; set; }
    }
    public RepositoryResult(bool isSuccess, string? errorMessage = null, int statusCode = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
    }
}
