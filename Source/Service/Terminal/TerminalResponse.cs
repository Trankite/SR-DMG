using StarRailDamage.Source.Service.Terminal.Abstraction;

namespace StarRailDamage.Source.Service.Terminal
{
    public class TerminalResponse : ITerminalResponse
    {
        public bool Success { get; }

        public string Message { get; } = string.Empty;

        public TerminalResponse() { }

        public TerminalResponse(bool success)
        {
            Success = success;
        }

        public TerminalResponse(bool success, string message) : this(success)
        {
            Message = message;
        }

        public static TerminalResponse<TContent> Create<TContent>(bool success, TContent? content)
        {
            return new TerminalResponse<TContent>(success, content);
        }

        public static TerminalResponse<TContent> Create<TContent>(bool success, string message, TContent? content)
        {
            return new TerminalResponse<TContent>(success, message, content);
        }
    }

    public class TerminalResponse<TContent> : TerminalResponse, ITerminalResponse<TContent>
    {
        public TContent? Content { get; set; }

        public TerminalResponse() { }

        public TerminalResponse(bool success) : base(success) { }

        public TerminalResponse(bool success, string message) : base(success, message) { }

        public TerminalResponse(bool success, TContent? content) : base(success)
        {
            Content = content;
        }

        public TerminalResponse(bool success, string message, TContent? content) : this(success, message)
        {
            Content = content;
        }
    }
}