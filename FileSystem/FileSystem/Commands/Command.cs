namespace FileSystem.Commands
{
    public abstract class Command : ICommand
    {
        public abstract void Execute(Models.FileSystem fileSystem);
    }
}
