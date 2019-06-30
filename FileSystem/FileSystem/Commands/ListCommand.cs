using System;
using System.Collections.Generic;
using FileSystem.Models;

namespace FileSystem.Commands
{
    public sealed class ListCommand : Command
    {
        public ListCommand(string directory)
        {
            Directory = directory == null ? string.Empty : directory.Trim();
        }

        public string Directory { get; }

        public override void Execute(Models.FileSystem fileSystem)
        {
            IEnumerable<File> fileList;
            try
            {
                fileList = fileSystem.ListDirectory(Directory);
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
                return;
            }
          
            foreach (var file in fileList)
            {
                if (file is Directory directory)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(directory.Name + " ");
                    Console.ResetColor();
                }
                else if (file is ContentFile contentFile)
                {
                    Console.Write(contentFile.Name + " ");
                }
            }

            Console.WriteLine();
        }
    }
}
