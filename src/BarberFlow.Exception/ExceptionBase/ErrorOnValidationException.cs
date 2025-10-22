using System.Net;

namespace BarberFlow.Exception.ExceptionBase;

public class ErrorOnValidationException : BarberFlowException
{
    private readonly List<string> _errors;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }


    public override List<string> GetErrors() => _errors;
}