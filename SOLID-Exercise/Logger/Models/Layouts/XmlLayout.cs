using LoggerLibrary.Contracts;
using System.Text;

namespace LoggerLibrary.Models
{
    public class XmlLayout : ILayout
    {
        public string GetString(string dateTime, ReportLevel severity, string message)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<log>");
            sb.AppendLine($"<date>{dateTime}</date>");
            sb.AppendLine($"<level>{severity}</level>");
            sb.AppendLine($"<message>{message}</message>");
            sb.AppendLine("<log>");

            return sb.ToString().TrimEnd();
        }
    }
}
