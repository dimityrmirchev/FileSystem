using System;

namespace FileSystem.Commands
{
    ///<summary>
    /// Represents a command used to clear the program's console.
    /// </summary>
    public sealed class ClearCommand : Command
    {
        /// <summary>
        /// Executes the clear command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
        public override void Execute(Models.FileSystem fileSystem)
        {
            Console.Clear();
        }
    }
}
