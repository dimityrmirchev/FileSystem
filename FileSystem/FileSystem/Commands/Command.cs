namespace FileSystem.Commands
{
    /// <summary>
    /// An abstraction used to build an executable commands hierarchy.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// The definition of the execute method.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command will be executed.</param>
        public abstract void Execute(Models.FileSystem fileSystem);
    }
}
