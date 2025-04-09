using EventApp.Application.Concrete;

namespace EventApp.Application.Results;

public class ServiceResult<T> : IServiceResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T Data { get; set; }

    public ServiceResult(bool success,string message,T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}