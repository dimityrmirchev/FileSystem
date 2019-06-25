﻿using System;
using FileSystem.Commands;
using FileSystem.Exceptions;
using FileSystem.Services;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var readLine = string.Empty;
            var fileSystem = new Models.FileSystem();
            var factory = new CommandFactory();
            while (!string.Equals(readLine, "exit"))
            {
                Console.Write("$ ");

                readLine = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(readLine))
                {
                    continue;
                }

                ICommand command;
                try
                {
                    command = factory.GetCommand(readLine);
                }
                catch (NotRecognizedCommandException commandException)
                {
                    Console.WriteLine(commandException.Message);
                    continue;
                }
                
                command.Execute(fileSystem);
            }
        }
    }
}
