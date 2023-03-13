using LoggerLibrary.Models;

namespace LoggerLibrary.Contracts
{
    public interface IAppender
    {
        ReportLevel ReportLevel { get; set; }
        void Append(string dateTime, ReportLevel severity, string message);
        int MessagesAppended { get; }
    }
}
