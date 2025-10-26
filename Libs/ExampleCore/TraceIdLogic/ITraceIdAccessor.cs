namespace ExampleCore.TraceIdLogic
{
    public interface ITraceIdAccessor
    {
        string? TraceId { get; set; }
    }
}
