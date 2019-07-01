namespace FileSystem.Commands
{
    ///<summary>
    /// Represents a command used to exit the program.
    /// </summary>
    public sealed class ExitCommand : Command
    {
        /// <summary>
        /// Executes the exit command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
        public override void Execute(Models.FileSystem fileSystem)
        {
            System.Environment.Exit(0);
        }
    }
}
