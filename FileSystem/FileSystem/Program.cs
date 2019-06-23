using System;
using FileSystem.Services;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystem = Models.FileSystem.Instance;

            var factory = new CommandFactory();
            var command = factory.GetCommand("cd test");
            command.Execute();

            command = factory.GetCommand("cd ..");
            command.Execute();
        }
    }
}
