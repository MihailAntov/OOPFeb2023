using LoggerLibrary.Contracts;
using System.Text;

namespace LoggerLibrary.Models
{
    public class Logger 
    {
        private List<IAppender> appenders;
        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders.ToList();
        }

        private void HandleErrorSeverity(string dateTime, ReportLevel severity, string message)
        {
            foreach (IAppender appender in appenders)
            {
                if (appender.ReportLevel <= severity)
                {
                    appender.Append(dateTime, severity, message);
                    
                }
            }
        }

        public IReadOnlyCollection<IAppender> Appenders
        {
            get { return appenders.AsReadOnly(); }

        }
        public void Info(string dateTime, string message)
        {
            HandleErrorSeverity(dateTime, ReportLevel.Info, message);
        }

        public void Warning(string dateTime, string message)
        {
            HandleErrorSeverity(dateTime, ReportLevel.Warning, message);
        }

        public void Error(string dateTime, string message)
        {
            HandleErrorSeverity(dateTime, ReportLevel.Error, message);
        }

        public void Critical(string dateTime, string message)
        {
            HandleErrorSeverity(dateTime, ReportLevel.Critical, message);
        }
        public void Fatal(string dateTime, string message)
        {
            HandleErrorSeverity(dateTime, ReportLevel.Fatal, message);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Logger info");

            foreach(IAppender appender in appenders)
            {
                sb.AppendLine(appender.ToString());
            }

            return sb.ToString();
        }

    }
}
