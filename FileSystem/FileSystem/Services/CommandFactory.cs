using System;
using System.Collections;
using System.Collections.Generic;
using FileSystem.Commands;
using FileSystem.Exceptions;

namespace FileSystem.Services
{
    public class CommandFactory
    {
        public ICommand GetCommand(string input)
        {
            var commandName = GetCommandName(input);
            var parameters = GetParameters(input);
            switch (commandName)
            {
                case "cd":
                    if (string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Cd command requires parameters.");
                    }
                    return new ChangeDirectoryCommand(parameters);
                case "mkdir":
                    if (string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Mkdir command requires parameters.");
                    }
                    return new MakeDirectoryCommand(parameters);
                case "pwd":
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Pwd command does not support parameters.");
                    }
                    return new PrintWorkingDirectoryCommand(parameters);
                case "ls":
                    return new ListCommand(parameters);
                case "rm":
                    if (string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Rm command requires parameters.");
                    }
                    return new RemoveCommand(parameters);
                case "cat":
                    if (string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Cat command requires parameters.");
                    }
                    return new ConcatenateCommand(parameters);
                case "clear":
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Clear command does not support parameters.");
                    }
                    return new ClearCommand(parameters);
                case "exit":
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Exit command does not support parameters.");
                    }
                    return new ExitCommand(parameters);
                default:
                    throw new NotRecognizedCommandException($"{commandName} was not recognized as a valid command.");
            }
        }

        private string GetCommandName(string job)
        {
            var trimmed = job.Trim();
            var commandLength = trimmed.IndexOf(' ') == -1
                ? trimmed.Length
                : trimmed.IndexOf(' ');
            var commandName = trimmed.Substring(0, commandLength);
            return commandName;
        }

        private string GetParameters(string job)
        {
            var trimmed = job.Trim();
            var splitter = trimmed.IndexOf(' ');
            return splitter == -1
                ? string.Empty
                : trimmed.Substring(splitter + 1, trimmed.Length - splitter - 1).Trim();
        }
    }
}
