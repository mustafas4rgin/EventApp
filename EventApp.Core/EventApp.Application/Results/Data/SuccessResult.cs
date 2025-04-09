

namespace EventApp.Application.Results;

public class SuccessResult<T> : ServiceResult<T>
{
    public SuccessResult(string message, T data)
        : base(true, message, data) { }
}