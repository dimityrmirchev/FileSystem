using System;
using FileSystem.Services;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var readLine = string.Empty;
            var fileSystem = new Models.FileSystem();
            var factory = new CommandFactory();
            while (!string.Equals(readLine, "exit"))
            {
                Console.Write("$ ");

                readLine = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(readLine))
                {
                    continue;
                }

                var command = factory.GetCommand(readLine);
                command.Execute(fileSystem);
            }
        }
    }
}
