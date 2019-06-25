using System;
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

            Assert.AreEqual("../../test", changeDirectoryCommand.Parameters);
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

            Assert.AreEqual("../../test", makeDirectoryCommand.Parameters);
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

            Assert.AreEqual("", printWorkingDirectoryCommand.Parameters);
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

            Assert.AreEqual("", listCommand.Parameters);
        }

        [TestMethod]
        public void CreateConcatenateCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("cat file1 > file2");
            var listCommand = (ConcatenateCommand)command;

            Assert.AreEqual("file1 > file2", listCommand.Parameters);
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
            var listCommand = (RemoveCommand)command;

            Assert.AreEqual("file1 file2", listCommand.Parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidRemoveCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("rm ");
        }
    }
}
