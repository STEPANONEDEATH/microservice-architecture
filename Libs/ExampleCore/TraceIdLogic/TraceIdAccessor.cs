using System.Threading;

namespace ExampleCore.TraceIdLogic
{
    public class TraceIdAccessor : ITraceIdAccessor
    {
        private static readonly AsyncLocal<string?> _current = new();

        public string? TraceId
        {
            get => _current.Value;
            set => _current.Value = value;
        }
    }
}
