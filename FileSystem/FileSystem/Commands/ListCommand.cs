using System;
using System.Collections.Generic;
using FileSystem.Models;

namespace FileSystem.Commands
{
    ///<summary>
    /// Represents a command used to list the content of a directory.
    /// </summary>
    public sealed class ListCommand : Command
    {
        /// <summary>
        /// Initializes a command of type ListCommand.
        /// </summary>
        /// <param name="directory">The path to the directory that is going to be listed.</param>
        public ListCommand(string directory)
        {
            Directory = directory == null ? string.Empty : directory.Trim();
        }

        /// <summary>
        /// Gets the path to the directory that is going to be listed.
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// Executes the list directory command.
        /// </summary>
        /// <param name="fileSystem">The file system on which the command is going to be executed.</param>
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
