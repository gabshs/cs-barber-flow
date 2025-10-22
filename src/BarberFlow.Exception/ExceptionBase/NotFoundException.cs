using System.Net;

namespace BarberFlow.Exception.ExceptionBase;

public class NotFoundException : BarberFlowException
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(message) { }

    public override List<string> GetErrors()
    {
        return [Message];
    }
}