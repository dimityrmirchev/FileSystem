using System;

namespace FileSystem.Commands
{
    ///<summary>
    /// Represents a command used to print the current working directory.
    /// </summary>
    public sealed class PrintWorkingDirectoryCommand : Command
    {
        /// <summary>
        /// Executes the print working directory command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
        public override void Execute(Models.FileSystem fileSystem)
        {
            Console.WriteLine(fileSystem.CurrentDirectoryPath);
        }
    }
}
