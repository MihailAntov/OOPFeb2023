using LoggerLibrary.Contracts;
using System.Text;

namespace LoggerLibrary.Models
{
    public class FileAppender : Appender
    {
        public readonly LogFile logFile;
        public FileAppender(ILayout layout, ReportLevel reportLevel) : base(layout, reportLevel)
        {
            logFile = new LogFile();
        }
        public FileAppender(ILayout layout) : this(layout, ReportLevel.Info)
        {

        }

        public override void Append(string dateTime, ReportLevel reportLevel, string message)
        {
            logFile.Write(layout.GetString(dateTime, reportLevel, message));
            MessagesAppended++;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.Append($", File size: {logFile.Size}");
            return sb.ToString().TrimEnd();
        }
    }
}
