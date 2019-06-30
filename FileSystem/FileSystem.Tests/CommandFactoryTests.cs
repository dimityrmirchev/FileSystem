using System;
using System.Linq;
using FileSystem.Commands;
using FileSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Tests
{
    [TestClass]
    public class CommandFactoryTests
    {
        [TestMethod]
        public void CreateChangeDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("cd ../../test");
            var changeDirectoryCommand = (ChangeDirectoryCommand)command;

            Assert.AreEqual("../../test", changeDirectoryCommand.Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidChangeDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("cd");
        }

        [TestMethod]
        public void CreateMakeDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("mkdir ../../test");
            var makeDirectoryCommand = (MakeDirectoryCommand)command;

            Assert.AreEqual("../../test", makeDirectoryCommand.Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidMakeDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("mkdir  ");
        }

        [TestMethod]
        public void CreatePrintWorkingDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("pwd");
            var printWorkingDirectoryCommand = (PrintWorkingDirectoryCommand)command;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidPrintWorkingDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("pwd invalid");
        }

        [TestMethod]
        public void CreateListCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("ls");
            var listCommand = (ListCommand)command;

            Assert.AreEqual("", listCommand.Directory);
        }


        [TestMethod]
        public void CreateListCommandForSpecificDirectory()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("ls /test/directory");
            var listCommand = (ListCommand)command;

            Assert.AreEqual("/test/directory", listCommand.Directory);
        }

        [TestMethod]
        public void CreateConcatenateCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("cat file1 file3 > file2");
            var concatenateCommand = (ConcatenateCommand)command;

            Assert.AreEqual("file2", concatenateCommand.OutputFile);
            Assert.IsTrue(concatenateCommand.InputFiles.Length == 2);
            Assert.IsTrue(concatenateCommand.InputFiles.Contains("file1"));
            Assert.IsTrue(concatenateCommand.InputFiles.Contains("file3"));
        }

        [TestMethod]
        public void CreateConcatenateCommandWithoutOutputFile()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("cat file1 file2");
            var concatenateCommand = (ConcatenateCommand)command;

            Assert.IsTrue(concatenateCommand.InputFiles.Length == 2);
            Assert.IsTrue(concatenateCommand.InputFiles.Contains("file1"));
            Assert.IsTrue(concatenateCommand.InputFiles.Contains("file2"));
        }

        [TestMethod]
        public void CreateConcatenateCommandWithoutInputFile()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("cat > outputFile");
            var concatenateCommand = (ConcatenateCommand) command;

            Assert.IsTrue(!concatenateCommand.InputFiles.Any());
            Assert.IsTrue(string.Equals(concatenateCommand.OutputFile, "outputFile"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEmptyConcatenateCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("cat");
        }

        [TestMethod]
        public void CreateRemoveCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("rm file1 file2");
            var removeCommand = (RemoveCommand)command;

            Assert.IsTrue(removeCommand.Paths.Length == 2);
            Assert.IsTrue(removeCommand.Paths.Contains("file1"));
            Assert.IsTrue(removeCommand.Paths.Contains("file2"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidRemoveCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("rm ");
        }

        [TestMethod]
        public void CreateClearCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("clear");
            var clearCommand = (ClearCommand)command;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidClearCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("clear test");
        }

        [TestMethod]
        public void CreateExitCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("exit");
            var exitCommand = (ExitCommand) command;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidExitCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("exit test");
        }
    }
}
