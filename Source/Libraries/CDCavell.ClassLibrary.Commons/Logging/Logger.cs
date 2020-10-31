using Microsoft.Extensions.Logging;
using System;

namespace CDCavell.ClassLibrary.Commons.Logging
{
    /// <summary>
    /// Global application logger
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 07/05/2020 | Initial build |~ 
    /// </revision>
    public class Logger
    {
        private const int TraceEvent = 1000;
        private const int DebugEvent = 1100;
        private const int InformationEvent = 2000;
        private const int WarningEvent = 3000;
        private const int ErrorEvent = 4000;
        private const int CriticalEvent = 5000;

        private readonly ILogger _logger;

        /// <summary>
        /// Initialize logger class
        /// </summary>
        /// <param name="logger">ILogger</param>
        /// <method>Logger(ILogger logger)</method>
        public Logger(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Information logging
        /// </summary>
        /// <param name="message">string</param>
        /// <method>Information(string message)</method>
        public void Information(string message)
        {
            _logger.LogInformation(InformationEvent, $"{DateTime.Now} {message.Clean()}");
        }

        /// <summary>
        /// Debug logging
        /// </summary>
        /// <param name="message">string</param>
        /// <method>Debug(string message)</method>
        public void Trace(string message)
        {
            _logger.LogTrace(TraceEvent, $"{DateTime.Now} {message.Clean()}");
        }

        /// <summary>
        /// Debug logging
        /// </summary>
        /// <param name="message">string</param>
        /// <method>Debug(string message)</method>
        public void Debug(string message)
        {
            _logger.LogDebug(DebugEvent, $"{DateTime.Now} {message.Clean()}");
        }

        /// <summary>
        /// Warning logging
        /// </summary>
        /// <param name="message">string</param>
        /// <method>Warning(string message)</method>
        public void Warning(string message)
        {
            _logger.LogWarning(WarningEvent, $"{DateTime.Now} {message.Clean()}");
        }

        /// <summary>
        /// Warning logging
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="message">string</param>
        /// <method>Critical(Exception exception, string message)</method>
        public void Critical(Exception exception, string message)
        {
            _logger.LogCritical(CriticalEvent, exception, message.Clean());

        }

        /// <summary>
        /// Exception logging
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="message">string</param>
        /// <method>Exception(Exception exception, string message = null)</method>
        public void Exception(Exception exception, string message = null)
        {
            _logger.LogError(ErrorEvent, $"{DateTime.Now} Exception thrown in application");

            if (!string.IsNullOrEmpty(message))
                _logger.LogError(ErrorEvent, "Message:              " + message.Clean());

            _logger.LogError(ErrorEvent, "Exception Message:    " + exception.Message);
            _logger.LogError(ErrorEvent, "Exception Source:     " + exception.Source);
            _logger.LogError(ErrorEvent, "Exception StackTrace: " + AsciiCodes.CRLF + exception.StackTrace + AsciiCodes.CRLF);

            if (exception.InnerException != null)
            {
                _logger.LogError(ErrorEvent, "InnerException Message:    " + exception.InnerException.Message);
                _logger.LogError(ErrorEvent, "InnerException Source:     " + exception.InnerException.Source);
                _logger.LogError(ErrorEvent, "InnerException StackTrace: " + AsciiCodes.CRLF + exception.InnerException.StackTrace + AsciiCodes.CRLF);
            }
        }

        /// <summary>
        /// Implementation of ILogger method
        /// </summary>
        /// <param name="logLevel">LogLevel</param>
        /// <param name="eventId">EventId</param>
        /// <param name="state">TState</param>
        /// <param name="exception">Exception</param>
        /// <param name="formatter">Func&lt;TState, Exception, string&gt;</param>
        /// <method>Log&lt;TState&gt;(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func&lt;TState, Exception, string&gt; formatter)</method>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Trace($"Log(logLevel: {logLevel.ToString()}; eventId: {eventId.ToString()}; state: {state.ToString()}; exception: {exception.ToString()}; formatter: {formatter.ToString()})");
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }

        /// <summary>
        /// Implementation of ILogger method
        /// </summary>
        /// <param name="logLevel">LogLevel</param>
        /// <method>IsEnabled(LogLevel logLevel)</method>
        /// <returns>bool</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            Trace($"IsEnabled(logLevel: {logLevel.ToString()})");
            return _logger.IsEnabled(logLevel);
        }

        /// <summary>
        /// Implementation of ILogger method
        /// </summary>
        /// <param name="state">TState</param>
        /// <method>BeginScope&lt;TState&gt;(TState state)</method>
        /// <returns>IDisposable</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            Trace($"BeginScope(state: {state.ToString()})");
            return _logger.BeginScope(state);
        }
    }
}
