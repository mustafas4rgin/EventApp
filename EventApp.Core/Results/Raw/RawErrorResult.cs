namespace EventApp.Core.Results;

public class RawErrorResult : RawServiceResult
{
    public RawErrorResult(string message) : base(false,message){}
}