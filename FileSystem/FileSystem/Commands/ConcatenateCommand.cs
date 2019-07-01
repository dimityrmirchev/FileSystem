using System;
using System.Linq;
using System.Text;
using FileSystem.Models;

namespace FileSystem.Commands
{
    ///<summary>
    /// Represents a command used to concatenate the content of a collection of files and output it to a target.
    /// </summary>
    public sealed class ConcatenateCommand : Command
    {
        /// <summary>
        /// Initializes a command of type ConcatenateCommand.
        /// </summary>
        /// <param name="parameters">Parameters specifying the input files and the output target.</param>
        public ConcatenateCommand(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
            {
                throw new ArgumentException("The \"cat\" command requires parameters.");
            }

            ParseParameters(parameters.Trim());
        }

        /// <summary>
        /// Gets the input files paths.
        /// </summary>
        public string[] InputFiles { get; private set; }

        /// <summary>
        /// Gets the output file path.
        /// </summary>
        public string OutputFile { get; private set; }

        /// <summary>
        /// Executes the concatenate command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
        public override void Execute(Models.FileSystem fileSystem)
        {
            var hasInputFiles = InputFiles.Any();
            var hasOutputFile = !string.IsNullOrWhiteSpace(OutputFile);

            if (hasInputFiles && hasOutputFile)
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
            else if (hasInputFiles)
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
            else if (hasOutputFile)
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
            else
            {
                Console.WriteLine("Couldn't execute cat command. Please check the validity of the passed parameters.");
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

            if (splitParameters.Length == 2 && GetOccurrencesCount(parameters, '>') == 1)
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
                throw new ArgumentException("Invalid parameters for cat command.");
            }
        }

        private static string ReadUserInput()
        {
            var stringBuilder = new StringBuilder();

            while (true)
            {
                var readLine = Console.ReadLine();
                if (string.Equals(readLine?.Trim(), "."))
                {
                    break;
                }

                stringBuilder.AppendLine(readLine);
            }

            return stringBuilder.ToString();
        }

        private static int GetOccurrencesCount(string text, char symbol)
        {
            var count = 0;
            foreach (var c in text)
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
