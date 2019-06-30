using System;

namespace FileSystem.Commands
{
    public sealed class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("The \"mkdir\" command requires a valid directory path.");
            }

            Path = path.Trim();
        }

        public string Path { get; }

        public override void Execute(Models.FileSystem fileSystem)
        {
            try
            {
                fileSystem.CreateDirectory(Path);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Console.WriteLine(invalidOperationException.Message);
            }
        }
    }
}
