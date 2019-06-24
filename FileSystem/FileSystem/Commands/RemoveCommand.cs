namespace FileSystem.Commands
{
    public class RemoveCommand : Command
    {
        public RemoveCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            var filePaths = Parameters.Split(' ');

        }
    }
}
