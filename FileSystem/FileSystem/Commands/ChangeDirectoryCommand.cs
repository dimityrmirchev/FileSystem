using System;

namespace FileSystem.Commands
{
    public sealed class ChangeDirectoryCommand : Command
    {
        public ChangeDirectoryCommand(string path)
        { 
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Cd command requires a valid path.");
            }

            Path = path.Trim();
        }

        public string Path { get; }

        public override void Execute(Models.FileSystem fileSystem)
        {
            try
            {
                fileSystem.ChangeDirectory(Path);
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
