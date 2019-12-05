using System;
using System.Diagnostics;
using System.Globalization;

namespace Logrila.Logging
{
    public class TraceLog : ILog
    {
        private readonly LogLevel _level;
        private readonly TraceSource _source;

        public TraceLog(TraceSource source)
        {
            _source = source;
            _level = LogLevel.FromSourceLevels(source.Switch.Level);
        }

        public bool IsTraceEnabled
        {
            get { return _level >= LogLevel.Trace; }
        }

        public bool IsDebugEnabled
        {
            get { return _level >= LogLevel.Debug; }
        }

        public bool IsInfoEnabled
        {
            get { return _level >= LogLevel.Info; }
        }

        public bool IsWarnEnabled
        {
            get { return _level >= LogLevel.Warn; }
        }

        public bool IsErrorEnabled
        {
            get { return _level >= LogLevel.Error; }
        }

        public bool IsFatalEnabled
        {
            get { return _level >= LogLevel.Fatal; }
        }

        public void Trace(object message)
        {
            if (!IsTraceEnabled)
                return;
            Log(LogLevel.Trace, message, null);
        }

        public void Trace(object message, Exception exception)
        {
            if (!IsTraceEnabled)
                return;
            Log(LogLevel.Trace, message, exception);
        }

        public void Trace(LogOutputProvider logOutputProvider)
        {
            if (!IsTraceEnabled)
                return;

            object obj = logOutputProvider();

            LogInternal(LogLevel.Trace, obj, null);
        }

        public void TraceFormat(string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            LogInternal(LogLevel.Trace, string.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            LogInternal(LogLevel.Trace, string.Format(formatProvider, format, args), null);
        }

        public void TraceFormat(Exception exception, string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            LogInternal(LogLevel.Trace, string.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        public void TraceFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            LogInternal(LogLevel.Trace, string.Format(formatProvider, format, args), exception);
        }

        public void Debug(object message)
        {
            if (!IsDebugEnabled)
                return;
            Log(LogLevel.Debug, message, null);
        }

        public void Debug(object message, Exception exception)
        {
            if (!IsDebugEnabled)
                return;
            Log(LogLevel.Debug, message, exception);
        }

        public void Debug(LogOutputProvider logOutputProvider)
        {
            if (!IsDebugEnabled)
                return;

            object obj = logOutputProvider();

            LogInternal(LogLevel.Debug, obj, null);
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            LogInternal(LogLevel.Debug, string.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            LogInternal(LogLevel.Debug, string.Format(formatProvider, format, args), null);
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            LogInternal(LogLevel.Debug, string.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            LogInternal(LogLevel.Debug, string.Format(formatProvider, format, args), exception);
        }

        public void Info(object message)
        {
            if (!IsInfoEnabled)
                return;
            Log(LogLevel.Info, message, null);
        }

        public void Info(object message, Exception exception)
        {
            if (!IsInfoEnabled)
                return;
            Log(LogLevel.Info, message, exception);
        }

        public void Info(LogOutputProvider logOutputProvider)
        {
            if (!IsInfoEnabled)
                return;

            object obj = logOutputProvider();

            LogInternal(LogLevel.Info, obj, null);
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            LogInternal(LogLevel.Info, string.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            LogInternal(LogLevel.Info, string.Format(formatProvider, format, args), null);
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            LogInternal(LogLevel.Info, string.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            LogInternal(LogLevel.Info, string.Format(formatProvider, format, args), exception);
        }

        public void Warn(object message)
        {
            if (!IsWarnEnabled)
                return;
            Log(LogLevel.Warn, message, null);
        }

        public void Warn(object message, Exception exception)
        {
            if (!IsWarnEnabled)
                return;
            Log(LogLevel.Warn, message, exception);
        }

        public void Warn(LogOutputProvider logOutputProvider)
        {
            if (!IsWarnEnabled)
                return;

            object obj = logOutputProvider();

            LogInternal(LogLevel.Warn, obj, null);
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            LogInternal(LogLevel.Warn, string.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            LogInternal(LogLevel.Warn, string.Format(formatProvider, format, args), null);
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            LogInternal(LogLevel.Warn, string.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            LogInternal(LogLevel.Warn, string.Format(formatProvider, format, args), exception);
        }

        public void Error(object message)
        {
            if (!IsErrorEnabled)
                return;
            Log(LogLevel.Error, message, null);
        }

        public void Error(object message, Exception exception)
        {
            if (!IsErrorEnabled)
                return;
            Log(LogLevel.Error, message, exception);
        }

        public void Error(LogOutputProvider logOutputProvider)
        {
            if (!IsErrorEnabled)
                return;

            object obj = logOutputProvider();

            LogInternal(LogLevel.Error, obj, null);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            LogInternal(LogLevel.Error, string.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            LogInternal(LogLevel.Error, string.Format(formatProvider, format, args), null);
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            LogInternal(LogLevel.Error, string.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            LogInternal(LogLevel.Error, string.Format(formatProvider, format, args), exception);
        }

        public void Fatal(object message)
        {
            if (!IsFatalEnabled)
                return;
            Log(LogLevel.Fatal, message, null);
        }

        public void Fatal(object message, Exception exception)
        {
            if (!IsFatalEnabled)
                return;
            Log(LogLevel.Fatal, message, exception);
        }

        public void Fatal(LogOutputProvider logOutputProvider)
        {
            if (!IsFatalEnabled)
                return;

            object obj = logOutputProvider();

            LogInternal(LogLevel.Fatal, obj, null);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            LogInternal(LogLevel.Fatal, string.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            LogInternal(LogLevel.Fatal, string.Format(formatProvider, format, args), null);
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            LogInternal(LogLevel.Fatal, string.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            LogInternal(LogLevel.Fatal, string.Format(formatProvider, format, args), exception);
        }

        private void Log(LogLevel level, object obj)
        {
            if (_level < level)
                return;

            LogInternal(level, obj, null);
        }

        private void Log(LogLevel level, object obj, Exception exception)
        {
            if (_level < level)
                return;

            LogInternal(level, obj, exception);
        }

        private void Log(LogLevel level, LogOutputProvider logOutputProvider)
        {
            if (_level < level)
                return;

            object obj = logOutputProvider();

            LogInternal(level, obj, null);
        }

        private void LogFormat(LogLevel level, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (_level < level)
                return;

            LogInternal(level, string.Format(formatProvider, format, args), null);
        }

        private void LogFormat(LogLevel level, string format, params object[] args)
        {
            if (_level < level)
                return;

            LogInternal(level, string.Format(format, args), null);
        }

        private void LogInternal(LogLevel level, object obj, Exception exception)
        {
            string message = obj == null ? string.Empty : obj.ToString();

            if (exception == null)
                _source.TraceEvent(level.TraceEventType, 0, message);
            else
                _source.TraceData(level.TraceEventType, 0, (object)message, (object)exception);
        }
    }
}
