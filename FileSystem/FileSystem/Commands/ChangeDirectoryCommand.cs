using System;

namespace FileSystem.Commands
{
    public class ChangeDirectoryCommand : Command
    {
        public ChangeDirectoryCommand(string path) : base(path)
        {

        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            try
            {
                fileSystem.ChangeDirectory(Parameters);
                Console.Write("$ ");
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
                Console.Write("$ ");
            }
        }
    }
}
