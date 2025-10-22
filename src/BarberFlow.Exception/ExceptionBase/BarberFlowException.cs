namespace BarberFlow.Exception.ExceptionBase;

public abstract class BarberFlowException : SystemException
{
    protected BarberFlowException(string message) : base(message) { }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}