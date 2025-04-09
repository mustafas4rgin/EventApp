using EventApp.Core.Concrete;

namespace EventApp.Core.Results;

public class RawServiceResult : IServiceResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;

    public RawServiceResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}