using System;
using System.Linq;
using System.Text;
using FileSystem.Models;

namespace FileSystem.Commands
{
    public sealed class ConcatenateCommand : Command
    {
        public ConcatenateCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            var splitParameters = Parameters.Split('>').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            if (splitParameters.Length == 2)
            {
                ConcatenateInputFilesIntoOutputFile(splitParameters, fileSystem);
                return;
            }

            if (splitParameters.Length == 1 && Parameters.Contains('>'))
            {
                var outputFilePath = splitParameters[0].Trim();
                ReadInputAndOutContentIntoFile(outputFilePath, fileSystem);
                return;
            }

            if (splitParameters.Length == 1 && !Parameters.Contains('>'))
            {
                var inputFilesPaths = splitParameters[0].Trim().Split(' ');
                ConcatenateFilesAndOutContentToConsole(inputFilesPaths, fileSystem);
            }
        }

        private void ConcatenateInputFilesIntoOutputFile(string[] splitParameters, Models.FileSystem fileSystem)
        {
            var inputFilesPaths = splitParameters[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray(); ;
            var stringBuilder = new StringBuilder();

            foreach (var path in inputFilesPaths)
            {
                if (fileSystem.TryGetFile(path.Trim(), out File file) && file is ContentFile contentFile)
                {
                    stringBuilder.AppendLine(contentFile.Content);
                }
                else
                {
                    Console.WriteLine($"File {path} couldn't be found");
                    return;
                }
            }

            var outputFilePath = splitParameters[1].Trim();
            try
            {
                fileSystem.CreateContentFile(outputFilePath, stringBuilder.ToString());
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

        private void ReadInputAndOutContentIntoFile(string outputFilePath, Models.FileSystem fileSystem)
        {
            var stringBuilder = new StringBuilder();
            var readLine = string.Empty;

            while (!string.Equals(readLine?.Trim(), "."))
            {
                readLine = Console.ReadLine();
                stringBuilder.AppendLine(readLine);
            }

            try
            {
                fileSystem.CreateContentFile(outputFilePath, stringBuilder.ToString());
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

        private void ConcatenateFilesAndOutContentToConsole(string[] inputFilesPaths, Models.FileSystem fileSystem)
        {
            var stringBuilder = new StringBuilder();

            foreach (var path in inputFilesPaths)
            {
                if (fileSystem.TryGetFile(path.Trim(), out File file) && file is ContentFile contentFile)
                {
                    stringBuilder.AppendLine(contentFile.Content);
                }
                else
                {
                    Console.WriteLine($"File {path} couldn't be found");
                    return;
                }
            }

            Console.Write(stringBuilder.ToString());
        }
    }
}
