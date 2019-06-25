﻿using System;

namespace FileSystem.Commands
{
    public class ClearCommand : Command
    {
        public ClearCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            Console.Clear();
        }
    }
}