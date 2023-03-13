using LoggerLibrary.Models;

namespace LoggerLibrary.Contracts
{
    public interface ILayout
    {
        string GetString(string dateTime,  ReportLevel severity, string message);
    }
}
