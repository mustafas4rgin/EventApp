namespace EventApp.Core.Results;

public class RawSuccessResult : RawServiceResult
{
    public RawSuccessResult(string message) : base(true,message){}
}