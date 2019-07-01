namespace FileSystem.Commands
{
    /// <summary>
    /// Provides a mechanism to determine if a class is a command and can be executed.
    /// </summary>
    public interface ICommand
    {
        void Execute(Models.FileSystem fileSystem);
    }
}
