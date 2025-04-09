using EventApp.Application.Results;

namespace EventApp.Application.Results;

public class RawSuccessResult : RawServiceResult
{
    public RawSuccessResult(string message) : base(true,message){}
}