using System;
using FileSystem.Models;

namespace FileSystem.Commands
{
    public class ListCommand : Command
    {
        public ListCommand(string parameters) : base(parameters)
        {
        }

        public override void Execute(Models.FileSystem fileSystem)
        {
            var fileList = fileSystem.ListCurrentDirectory();
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
        }
    }
}
