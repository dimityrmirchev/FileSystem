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
        public void CreateMakeDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("mkdir ../../test");
            var changeDirectoryCommand = (MakeDirectoryCommand)command;

            Assert.AreEqual("../../test", changeDirectoryCommand.Parameters);
        }

        [TestMethod]
        public void CreatePrintWorkingDirectoryCommand()
        {
            var factory = new CommandFactory();
            var command = factory.GetCommand("pwd");
            var changeDirectoryCommand = (PrintWorkingDirectoryCommand)command;

            Assert.AreEqual("", changeDirectoryCommand.Parameters);
        }
    }
}
