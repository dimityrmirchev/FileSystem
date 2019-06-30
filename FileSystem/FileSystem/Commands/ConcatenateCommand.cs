﻿using System;
using System.Linq;
using System.Text;
using FileSystem.Models;

namespace FileSystem.Commands
{
    public sealed class ConcatenateCommand : Command
    {
        public ConcatenateCommand(string parameters)
        {
            if (string.IsNullOrEmpty(parameters))
            {
                throw new ArgumentException("Cat command requires parameters.");
            }

            ParseParameters(parameters.Trim());
        }

        public string[] InputFiles { get; private set; }

        public string OutputFile { get; private set; }

        public override void Execute(Models.FileSystem fileSystem)
        {
            if (InputFiles.Any() && !string.IsNullOrWhiteSpace(OutputFile))
            {
                try
                {
                    var concatenatedContent = ConcatenateFiles(fileSystem);
                    fileSystem.CreateContentFile(OutputFile, concatenatedContent);
                }
                catch (InvalidOperationException operationException)
                {
                    Console.WriteLine(operationException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
            }
            else if (InputFiles.Any() && string.Equals(OutputFile, string.Empty))
            {
                try
                {
                    var concatenatedContent = ConcatenateFiles(fileSystem);
                    Console.Write(concatenatedContent);
                }
                catch (InvalidOperationException operationException)
                {
                    Console.WriteLine(operationException.Message);
                }
            }
            else if (!InputFiles.Any() && !string.IsNullOrWhiteSpace(OutputFile))
            {
                var userInput = ReadUserInput();

                try
                {
                    fileSystem.CreateContentFile(OutputFile, userInput);
                }
                catch (InvalidOperationException operationException)
                {
                    Console.WriteLine(operationException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
            }
        }

        private string ConcatenateFiles(Models.FileSystem fileSystem)
        {
            var stringBuilder = new StringBuilder();

            foreach (var path in InputFiles)
            {
                if (fileSystem.TryGetFile(path.Trim(), out File file) && file is ContentFile contentFile)
                {
                    stringBuilder.AppendLine(contentFile.Content);
                }
                else
                {
                    throw new InvalidOperationException($"File {path} couldn't be found");
                }
            }

            return stringBuilder.ToString();
        }

        private void ParseParameters(string parameters)
        {
            var splitParameters = parameters.Split('>').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            if (splitParameters.Length == 2 && ContainsSymbolCount(parameters, '>') == 1)
            {
                InputFiles = splitParameters[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                OutputFile = splitParameters[1].Trim();
            }
            else if (splitParameters.Length == 1 && parameters.Contains('>') && parameters.StartsWith('>'))
            {
                InputFiles = new string[0];
                OutputFile = splitParameters[0].Trim();
            }
            else if (splitParameters.Length == 1 && !parameters.Contains('>'))
            {
                InputFiles = splitParameters[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                OutputFile = string.Empty;
            }
            else
            {
                throw new ArgumentException("Invalid parameters for cat command");
            }
        }

        private static string ReadUserInput()
        {
            var stringBuilder = new StringBuilder();
            var readLine = string.Empty;

            while (!string.Equals(readLine?.Trim(), "."))
            {
                readLine = Console.ReadLine();
                stringBuilder.AppendLine(readLine);
            }

            return stringBuilder.ToString();
        }

        private static int ContainsSymbolCount(string input, char symbol)
        {
            var count = 0;
            foreach (var c in input)
            {
                if (c == symbol)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
