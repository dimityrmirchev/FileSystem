namespace FileSystem.Commands
{
    public sealed class ExitCommand : Command
    {
        public override void Execute(Models.FileSystem fileSystem)
        {
            System.Environment.Exit(0);
        }
    }
}
