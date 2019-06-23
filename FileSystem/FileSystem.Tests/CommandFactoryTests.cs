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
            var changeDirectoryCommand = (ChangeDirectoryCommand) command;
            
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
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidListCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("ls  invalid");
        }
    }
}
