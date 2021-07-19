using System;

namespace Common.Log4
{
    internal class LogoCommon
    {

    }
    public enum LogLevel
    {
        All = 0,
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
        Off
    }
    public interface ILogService
    {
        void SetLogLevel(LogLevel level);
        LogLevel GetLogLevel();
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Error(string message, Exception ex);
        void Fatal(string message);
        void Fatal(string message, Exception ex);
    }
}
