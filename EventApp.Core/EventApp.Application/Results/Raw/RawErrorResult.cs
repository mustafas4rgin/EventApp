using EventApp.Application.Results;

namespace EventApp.Application.Results;

public class RawErrorResult : RawServiceResult
{
    public RawErrorResult(string message) : base(false,message){}
}