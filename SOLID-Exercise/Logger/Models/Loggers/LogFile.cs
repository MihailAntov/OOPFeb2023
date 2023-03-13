
using System.Text;


namespace LoggerLibrary.Models
{
    public class LogFile
    {
        private StringBuilder stringBuilder;
        public LogFile()
        {
            stringBuilder = new StringBuilder();
        }

        public void Write(string line)
        {
            stringBuilder.AppendLine(line);
        }
        public int Size
        {
            get
            {
                return stringBuilder.ToString()
                    .Where(c => char.IsLetter(c))
                    .Select(c => (int)c)
                    .Sum();
            }
        }

        public override string ToString()
        {
            return stringBuilder.ToString().TrimEnd();
        }

    }
}
