using LoggerLibrary.Contracts;

namespace LoggerLibrary.Models;

public class ConsoleAppender : Appender
{
    public ConsoleAppender(ILayout layout) : base(layout){}

    public ConsoleAppender(ILayout layout, ReportLevel reportLevel) : base(layout, reportLevel){}

    public override void Append(string dateTime, ReportLevel reportLevel, string message)
    {
        Console.WriteLine(layout.GetString(dateTime,reportLevel,message));
        MessagesAppended++;
    }
}
