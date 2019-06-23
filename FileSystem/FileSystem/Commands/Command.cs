namespace FileSystem.Commands
{
    public abstract class Command : ICommand
    {
        protected Command(string parameters)
        {
            Parameters = parameters;
        }

        public string Parameters { get; }

        public abstract void Execute(Models.FileSystem fileSystem);
    }
}
