using System;

namespace FileSystem.Commands
{
    public class ChangeDirectoryCommand : Command
    {
        public ChangeDirectoryCommand(string path) : base(path)
        {

        }

        public override void Execute()
        {
            try
            {
                Models.FileSystem.Instance.ChangeDirectory(Parameters);
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
