using LoggerLibrary.Contracts;
using LoggerLibrary.Models;


namespace LoggerLibrary.Core
{
    public class Engine
    {
        private Logger logger;
        public Engine()
        {
            
        }
        public void Run()
        {
            CreateAppenders();
            HandleInput();
            Report();
        }
        public static IAppender CreateAppender()
        {
            string[] appenderArgs = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IAppender appender = null!;
            ILayout layout = null!;

            string appenderType = appenderArgs[0];
            string layoutType = appenderArgs[1];

            if (layoutType == "SimpleLayout")
            {
                layout = new SimpleLayout();
            }
            else if (layoutType == "XmlLayout")
            {
                layout = new XmlLayout();
            }

            if (appenderType == "ConsoleAppender")
            {
                appender = new ConsoleAppender(layout);
            }
            else if (appenderType == "FileAppender")
            {
                appender = new FileAppender(layout);
            }

            if(appenderArgs.Length == 3)
            {
                appender.ReportLevel = (ReportLevel)Enum.Parse(typeof(ReportLevel), appenderArgs[2],true);
            }

            return appender;
        }

        public void CreateAppenders()
        {
            List<IAppender> appenders = new List<IAppender>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                appenders.Add(CreateAppender());
            }

            logger = new Logger(appenders.ToArray());
        }

        public void HandleInput()
        {
            string input;

            while((input = Console.ReadLine())!= "END")
            {
                string[] inputArgs = input.Split("|");
                string dateTime = inputArgs[1];
                string message = inputArgs[2];

                switch(inputArgs[0].ToLower())
                {
                    case "info":
                        logger.Info(dateTime, message);
                        break;
                    case "warning":
                        logger.Warning(dateTime, message);
                        break;
                    case "error":
                        logger.Error(dateTime, message);
                        break;
                    case "critical":
                        logger.Critical(dateTime, message);
                        break;
                    case "fatal":
                        logger.Fatal(dateTime, message);
                        break;
                }
            }
        }

        public void Report()
        {
            Console.WriteLine("Logger info");
            foreach(IAppender appender in logger.Appenders)
            {
                Console.WriteLine(appender.ToString());
            }
        }


    }
}
