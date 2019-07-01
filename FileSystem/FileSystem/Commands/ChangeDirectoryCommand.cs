using System;

namespace FileSystem.Commands
{
    ///<summary>
    /// Represents a command used to change the current directory of a file system.
    /// </summary>
    public sealed class ChangeDirectoryCommand : Command
    {
        /// <summary>
        /// Initializes a command of type ChangeDirectoryCommand.
        /// </summary>
        /// <param name="path">The path to navigate to.</param>
        public ChangeDirectoryCommand(string path)
        { 
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("The \"cd\" command requires a valid path.");
            }

            Path = path.Trim();
        }

        /// <summary>
        /// Gets the path to navigate to.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Executes the change directory command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
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
