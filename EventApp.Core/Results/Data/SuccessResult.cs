using EventApp.Core.Concrete;

namespace EventApp.Core.Results;

public class SuccessResult<T> : ServiceResult<T>
{
    public SuccessResult(string message, T data)
        : base(true, message, data) { }
}