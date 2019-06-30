using System;

namespace FileSystem.Commands
{
    public sealed class ClearCommand : Command
    {
        public override void Execute(Models.FileSystem fileSystem)
        {
            Console.Clear();
        }
    }
}
