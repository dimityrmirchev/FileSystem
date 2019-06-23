using System;
using FileSystem.Services;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystem = new Models.FileSystem();

            var factory = new CommandFactory();
            var command = factory.GetCommand("cd test");
            command.Execute(fileSystem);

            command = factory.GetCommand("cd ..");
            command.Execute(fileSystem);
        }
    }
}
