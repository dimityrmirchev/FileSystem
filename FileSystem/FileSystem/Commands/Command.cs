namespace FileSystem.Commands
{
    public abstract class Command : ICommand
    {
        protected readonly string Parameters;

        protected Command(string parameters)
        {
            Parameters = parameters;
        }

        public abstract void Execute();
    }
}
