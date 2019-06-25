using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.Commands
{
    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            try
            {
                fileSystem.CreateDirectory(Parameters);
                Console.Write("$ ");
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
                Console.Write("$ ");
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Console.WriteLine(invalidOperationException.Message);
                Console.Write("$ ");
            }
        }
    }
}
