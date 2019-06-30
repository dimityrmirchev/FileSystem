﻿using System;

namespace FileSystem.Commands
{
    public sealed class PrintWorkingDirectoryCommand : Command
    {
        public PrintWorkingDirectoryCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            Console.WriteLine(fileSystem.CurrentDirectoryPath);
        }
    }
}
