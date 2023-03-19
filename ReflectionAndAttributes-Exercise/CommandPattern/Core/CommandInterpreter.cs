
using System.Linq;
using System;
using System.Reflection;

using CommandPattern.Core.Contracts;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] inputArgs = args.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

            string command = inputArgs[0];
            string[] commandArgs = inputArgs.Skip(1).ToArray();

            Type commandType = Assembly.GetEntryAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{command}Command");

            ICommand instance = (ICommand)Activator.CreateInstance(commandType);

            string result = instance.Execute(commandArgs);

            return result;
        }
    }
}
