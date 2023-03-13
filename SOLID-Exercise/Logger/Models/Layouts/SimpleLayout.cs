using LoggerLibrary.Contracts;

namespace LoggerLibrary.Models;

public class SimpleLayout : ILayout
{
    public string GetString(string dateTime, ReportLevel severity, string message)
    {
        return $"{dateTime} - {severity.ToString().ToUpper()} - {message}";
    }
}
