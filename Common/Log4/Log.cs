using log4net;
using log4net.Core;
using System;

namespace Common.Log4
{
    public class Log : ILogService
    {
        private readonly ILog _logger;

        public Level CurLogLevel
        {
            get
            {
                return ((log4net.Repository.Hierarchy.Logger)_logger.Logger).Level;
            }
            set
            {
                ((log4net.Repository.Hierarchy.Logger)_logger.Logger).Level = value;
            }
        }

        internal Log(ILog logger)
        {
            this._logger = logger;
        }

        public void SetLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.All:
                    CurLogLevel = Level.All;
                    break;
                case LogLevel.Debug:
                    CurLogLevel = Level.Debug;
                    break;
                case LogLevel.Info:
                    CurLogLevel = Level.Info;
                    break;
                case LogLevel.Warn:
                    CurLogLevel = Level.Warn;
                    break;
                case LogLevel.Error:
                    CurLogLevel = Level.Error;
                    break;
                case LogLevel.Fatal:
                    CurLogLevel = Level.Fatal;
                    break;
                default:
                    CurLogLevel = Level.Info;
                    break;
            }

        }

        public LogLevel GetLogLevel()
        {
            if (CurLogLevel == Level.All)
            {
                return LogLevel.All;
            }
            else if (CurLogLevel == Level.Debug)
            {
                return LogLevel.Debug;
            }
            else if (CurLogLevel == Level.Info)
            {
                return LogLevel.Info;
            }
            else if (CurLogLevel == Level.Warn)
            {
                return LogLevel.Warn;
            }
            else if (CurLogLevel == Level.Error)
            {
                return LogLevel.Error;
            }
            else if (CurLogLevel == Level.Fatal)
            {
                return LogLevel.Fatal;
            }

            return LogLevel.Info;
        }

        public void Debug(string message)
        {
            this._logger.Debug(message);
        }

        public void Info(string message)
        {
            this._logger.Info(message);
        }

        public void Warn(string message)
        {
            this._logger.Warn(message);
        }

        public void Error(string message)
        {
            this._logger.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            this._logger.Error(message, ex);
        }

        public void Fatal(string message)
        {
            this._logger.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            this._logger.Fatal(message, ex);
        }


    }
}
