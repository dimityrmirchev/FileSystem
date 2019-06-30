using System;

namespace FileSystem.Commands
{
    public sealed class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            try
            {
                fileSystem.CreateDirectory(Parameters);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Console.WriteLine(invalidOperationException.Message);
            }
        }
    }
}
