using LoggerLibrary.Contracts;
using System.Text;

namespace LoggerLibrary.Models;

public abstract class Appender : IAppender
{
    protected ReportLevel reportLevel;
    protected ILayout layout;

    public Appender(ILayout layout, ReportLevel reportLevel)
    {
        this.ReportLevel = reportLevel;
        this.layout = layout;
    }

    public Appender(ILayout layout) : this(layout, 0)
    {

    }
    public int MessagesAppended { get; protected set; }

    public ReportLevel ReportLevel
    {
        get { return reportLevel; }
        set { reportLevel = value; }
    }
    public abstract void Append(string dateTime, ReportLevel reportLevel, string message);

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"Appender type: {this.GetType().Name}, Layout type: {this.layout.GetType().Name}, Report level : {reportLevel.ToString().ToUpper()}, Messages appended: {MessagesAppended}");
        return sb.ToString();
    }
}
