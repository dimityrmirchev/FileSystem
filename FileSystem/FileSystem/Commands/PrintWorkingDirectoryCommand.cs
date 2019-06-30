using System;

namespace FileSystem.Commands
{
    public sealed class PrintWorkingDirectoryCommand : Command
    {
        public override void Execute(Models.FileSystem fileSystem)
        {
            Console.WriteLine(fileSystem.CurrentDirectoryPath);
        }
    }
}
