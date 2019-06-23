using System;
using System.Collections;
using System.Collections.Generic;
using FileSystem.Commands;

namespace FileSystem.Services
{
    public class CommandFactory
    { 
        public ICommand GetCommand(string job)
        {
            var trimmed = job.Trim();
            var command = trimmed.Substring(0, trimmed.IndexOf(' '));
            var parameters = trimmed.Substring(trimmed.IndexOf(' ') + 1, trimmed.Length - command.Length - 1);
            switch (command)
            {
                case "cd":
                    return new ChangeDirectoryCommand(parameters);
                case "mkdir":
                    return new MakeDirectoryCommand(parameters);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
