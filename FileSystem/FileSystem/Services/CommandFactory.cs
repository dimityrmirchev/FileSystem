﻿using System;
using FileSystem.Commands;
using FileSystem.Exceptions;

namespace FileSystem.Services
{
    public sealed class CommandFactory
    {
        public ICommand GetCommand(string input)
        {
            var commandName = GetCommandName(input);
            var parameters = GetParameters(input);

            switch (commandName)
            {
                case "cd":
                    return new ChangeDirectoryCommand(parameters);
                case "pwd":
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Pwd command does not support parameters.");
                    }
                    return new PrintWorkingDirectoryCommand();
                case "ls":
                    return new ListCommand(parameters);
                case "mkdir":
                    return new MakeDirectoryCommand(parameters);
                case "cat":
                    return new ConcatenateCommand(parameters);
                case "rm":
                    return new RemoveCommand(parameters);
                case "clear":
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Clear command does not support parameters.");
                    }
                    return new ClearCommand();
                case "exit":
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        throw new ArgumentException("Exit command does not support parameters.");
                    }
                    return new ExitCommand();
                default:
                    throw new NotRecognizedCommandException($"{commandName} was not recognized as a valid command.");
            }
        }

        private static string GetCommandName(string input)
        {
            var trimmedInput = input.Trim();
            var commandLength = trimmedInput.IndexOf(' ') == -1
                ? trimmedInput.Length
                : trimmedInput.IndexOf(' ');
            var commandName = trimmedInput.Substring(0, commandLength);

            return commandName;
        }

        private static string GetParameters(string input)
        {
            var trimmedInput = input.Trim();
            var splitter = trimmedInput.IndexOf(' ');

            return splitter == -1
                ? string.Empty
                : trimmedInput.Substring(splitter + 1, trimmedInput.Length - splitter - 1).Trim();
        }
    }
}
