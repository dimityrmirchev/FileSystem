using System;

namespace FileSystem.Commands
{
    /// <summary>
    /// Represents a command used to delete files in the file system.
    /// </summary>
    public sealed class RemoveCommand : Command
    {
        /// <summary>
        /// Initializes a command of type RemoveCommand.
        /// </summary>
        /// <param name="paths">The paths of the files that are going to be removed.</param>
        public RemoveCommand(string paths)
        {
            if (string.IsNullOrWhiteSpace(paths))
            {
                throw new ArgumentException("The \"rm\" command requires valid file paths.");
            }

            Paths = paths.Split(' ');
        }

        /// <summary>
        /// Gets the paths of the files that are going to be removed.
        /// </summary>
        public string[] Paths { get; }

        /// <summary>
        /// Executes the remove command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
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
