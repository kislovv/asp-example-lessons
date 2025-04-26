using System.Text.RegularExpressions;

namespace ServicesExample.Configurations.Logging;

public class SecureLogger<T>(ILoggerFactory loggerFactory) : ILogger<T>
{
    private readonly ILogger<T> _innerLogger = loggerFactory.CreateLogger<T>();

    public bool IsEnabled(LogLevel logLevel)
    {
        return _innerLogger.IsEnabled(logLevel);
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return _innerLogger.BeginScope(state);
    }
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception exception, Func<TState, Exception, string> formatter)
    {
        var message = formatter(state, exception);
        var securedMessage = MaskSensitiveData(message);
        _innerLogger.Log(logLevel, eventId, state, exception, (s, e) => securedMessage);
    }

    private string MaskSensitiveData(string message)
    {
        if (string.IsNullOrEmpty(message))
            return message;

        message = Regex.Replace(message, @"(token*\s+)[\w\.\-]+", "$1[PROTECTED]", RegexOptions.IgnoreCase);
        message = Regex.Replace(message, @"(""password""\s*:\s*"")[^""]+(""|\s|,)", "$1[HIDDEN]$2", RegexOptions.IgnoreCase);

        return message;
    }
}