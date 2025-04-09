using EventApp.Core.Concrete;

namespace EventApp.Core.Results;

public class ErrorResult<T> : ServiceResult<T>
{
    public ErrorResult(string message)
        : base(false, message, default!) { }
}