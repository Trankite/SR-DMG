using System.Runtime.ExceptionServices;

namespace StarRailDamage.Source.Core.Abstraction
{
    public interface IExceptionCapture
    {
        ExceptionDispatchInfo? Exception { get; set; }
    }
}