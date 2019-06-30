using System;

namespace FileSystem.Commands
{
    public sealed class RemoveCommand : Command
    {
        public RemoveCommand(string paths)
        {
            if (string.IsNullOrWhiteSpace(paths))
            {
                throw new ArgumentException("The \"rm\" command requires valid file paths.");
            }

            Paths = paths.Split(' ');
        }

        public string[] Paths { get; }

        public override void Execute(Models.FileSystem fileSystem)
        {
            try
            {
                fileSystem.RemoveContentFiles(Paths);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Console.WriteLine(invalidOperationException.Message);
            }
        }
    }
}
