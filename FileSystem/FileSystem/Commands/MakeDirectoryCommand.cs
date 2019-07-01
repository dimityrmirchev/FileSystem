using System;

namespace FileSystem.Commands
{
    ///<summary>
    /// Represents a command used to create a new directory.
    /// </summary>
    public sealed class MakeDirectoryCommand : Command
    {
        /// <summary>
        /// Initializes a command of type MakeDirectoryCommand.
        /// </summary>
        /// <param name="path">The path to the directory that is going to be created.</param>
        public MakeDirectoryCommand(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("The \"mkdir\" command requires a valid directory path.");
            }

            Path = path.Trim();
        }

        /// <summary>
        /// Gets the path to the directory that is going to be created.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Executes the make directory command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
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
