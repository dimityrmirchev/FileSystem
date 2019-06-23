namespace FileSystem.Commands
{
    public interface ICommand
    {
        void Execute(Models.FileSystem fileSystem);
    }
}
