using StarRailDamage.Source.Core.Abstraction;

namespace StarRailDamage.Source.Service.IO.FiledStream.Abstraction
{
    public interface IFiledStreamStatus : IExceptionCapture
    {
        bool Success { get; }
    }
}