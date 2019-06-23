using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Tests
{
    [TestClass]
    public class FileSystemTests
    {
        [TestMethod]
        public void AddDirectoryFullPathAndTryGetIt()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");

            if (fileSystem.TryGetDirectory("/directory", out var directory))
            {
                Assert.AreEqual("directory", directory.Name);
                Assert.AreEqual("/directory", directory.Path);
            }
            else
            {
                Assert.Fail("Directory was not added or couldn't be found in the tree.");
            }
        }

        [TestMethod]
        public void AddTwoDirectoriesFullPath()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/test");

            if (fileSystem.TryGetDirectory("/directory", out var directory1))
            {
                Assert.AreEqual("directory", directory1.Name);
                Assert.AreEqual("/directory", directory1.Path);
            }
            else
            {
                Assert.Fail("Directory was not added or couldn't be found in the tree.");
            }

            if (fileSystem.TryGetDirectory("/directory/test", out var directory2))
            {
                Assert.AreEqual("test", directory2.Name);
                Assert.AreEqual("/directory/test", directory2.Path);
            }
            else
            {
                Assert.Fail("Directory was not added or couldn't be found in the tree.");
            }
        }

        [TestMethod]
        public void AddTwoDirectoriesRelativePath()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("directory");
            fileSystem.AddDirectory("directory/test");

            if (fileSystem.TryGetDirectory("/directory", out var directory1))
            {
                Assert.AreEqual("directory", directory1.Name);
                Assert.AreEqual("/directory", directory1.Path);
            }
            else
            {
                Assert.Fail("Directory was not added or couldn't be found in the tree.");
            }

            if (fileSystem.TryGetDirectory("/directory/test", out var directory2))
            {
                Assert.AreEqual("test", directory2.Name);
                Assert.AreEqual("/directory/test", directory2.Path);
            }
            else
            {
                Assert.Fail("Directory was not added or couldn't be found in the tree.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryAddExistingDirectory()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("directory");
            fileSystem.AddDirectory("directory");
        }

        [TestMethod]
        public void ChangeDirectory()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");

            Assert.AreEqual("/", fileSystem.CurrentDirectoryPath);

            fileSystem.ChangeDirectory("/directory");

            Assert.AreEqual("/directory", fileSystem.CurrentDirectoryPath);

            fileSystem.ChangeDirectory("testDirectory");

            Assert.AreEqual("/directory/testDirectory", fileSystem.CurrentDirectoryPath);

            fileSystem.ChangeDirectory("..");

            Assert.AreEqual("/directory", fileSystem.CurrentDirectoryPath);

            fileSystem.ChangeDirectory("../directory/testDirectory");

            Assert.AreEqual("/directory/testDirectory", fileSystem.CurrentDirectoryPath);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryNavigateToInvalidDirectory()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");

            fileSystem.ChangeDirectory("/directory1");
        }
    }
}
