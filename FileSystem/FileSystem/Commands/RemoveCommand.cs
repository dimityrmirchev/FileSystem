using System;

namespace FileSystem.Commands
{
    public sealed class RemoveCommand : Command
    {
        public RemoveCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            var filePaths = Parameters.Split(' ');

            try
            {
                fileSystem.RemoveContentFiles(filePaths);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Console.WriteLine(invalidOperationException.Message);
            }
        }
    }
}
