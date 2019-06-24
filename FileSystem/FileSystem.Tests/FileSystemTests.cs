using System;
using System.Linq;
using FileSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Tests
{
    [TestClass]
    public class FileSystemTests
    {
        [TestMethod]
        public void CheckDefaultDirectory()
        {
            var fileSystem = new Models.FileSystem();
            if (fileSystem.TryGetFile("/", out var directory))
            {
                Assert.AreEqual("", directory.Name);
                Assert.AreEqual("/", directory.Path);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddDirectoryFullPathAndTryGetIt()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");

            if (fileSystem.TryGetFile("/directory", out var directory))
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

            if (fileSystem.TryGetFile("/directory", out var directory1))
            {
                Assert.AreEqual("directory", directory1.Name);
                Assert.AreEqual("/directory", directory1.Path);
            }
            else
            {
                Assert.Fail("Directory was not added or couldn't be found in the tree.");
            }

            if (fileSystem.TryGetFile("/directory/test", out var directory2))
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

            if (fileSystem.TryGetFile("/directory", out var directory1))
            {
                Assert.AreEqual("directory", directory1.Name);
                Assert.AreEqual("/directory", directory1.Path);
            }
            else
            {
                Assert.Fail("Directory was not added or couldn't be found in the tree.");
            }

            if (fileSystem.TryGetFile("/directory/test", out var directory2))
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

            fileSystem.ChangeDirectory("/");

            Assert.AreEqual("/", fileSystem.CurrentDirectoryPath);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ChangeDirectoryToEmpty()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");

            fileSystem.ChangeDirectory("  ");
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

        [TestMethod]
        public void ListDirectoryFullPath()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");
            fileSystem.AddDirectory("/directory/testDirectory1");
            fileSystem.AddDirectory("/directory/testDirectory2");

            var listedDirectory = fileSystem.ListDirectory("/directory").ToList();

            Assert.AreEqual(3, listedDirectory.Count);
            Assert.IsTrue(listedDirectory.FirstOrDefault(f => f.Path == "/directory/testDirectory") != null);
            Assert.IsTrue(listedDirectory.FirstOrDefault(f => f.Path == "/directory/testDirectory1") != null);
            Assert.IsTrue(listedDirectory.FirstOrDefault(f => f.Path == "/directory/testDirectory2") != null);
        }

        [TestMethod]
        public void ListDirectoryRelative()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");
            fileSystem.AddDirectory("/directory/testDirectory1");
            fileSystem.AddDirectory("/directory/testDirectory2");

            var listedDirectory = fileSystem.ListDirectory("directory").ToList();

            Assert.AreEqual(3, listedDirectory.Count);
            Assert.IsTrue(listedDirectory.FirstOrDefault(f => f.Path == "/directory/testDirectory") != null);
            Assert.IsTrue(listedDirectory.FirstOrDefault(f => f.Path == "/directory/testDirectory1") != null);
            Assert.IsTrue(listedDirectory.FirstOrDefault(f => f.Path == "/directory/testDirectory2") != null);
        }

        [TestMethod]
        public void ListEmptyDirectory()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");

            var listedDirectory = fileSystem.ListDirectory("/directory/testDirectory").ToList();

            Assert.AreEqual(0, listedDirectory.Count);
        }

        [TestMethod]
        public void ListCurrentDirectory()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");

            var listedDirectory = fileSystem.ListDirectory("").ToList();

            Assert.AreEqual(1, listedDirectory.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ListInvalidDirectory()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/testDirectory");
            fileSystem.AddDirectory("/directory/testDirectory1");
            fileSystem.AddDirectory("/directory/testDirectory2");

            var listedDirectory = fileSystem.ListDirectory("/directory/123").ToList();
        }

        [TestMethod]
        public void RemoveContentFiles()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");

            if (fileSystem.TryGetFile("/directory", out var file) && file is Directory directory)
            {
                directory.AddChild(new ContentFile("/directory/file1", "test", directory));
                directory.AddChild(new ContentFile("/directory/file2", "test", directory));

                var paths = new[] { "/directory/file1", "/directory/file2" };

                Assert.IsTrue(2 == directory.GetChildren().Count());

                fileSystem.RemoveContentFiles(paths);

                Assert.IsTrue(!directory.GetChildren().Any());
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveContentFilesFromDifferentDirectories()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            fileSystem.AddDirectory("/directory/directory1");
            if (fileSystem.TryGetFile("/directory", out var file) && file is Directory directory)
            {
                if (fileSystem.TryGetFile("/directory/directory1", out var file1) && file1 is Directory directory1)
                {
                    directory.AddChild(new ContentFile("/directory/file1", "test", directory));
                    directory1.AddChild(new ContentFile("/directory/directory1/file2", "test", directory1));

                    var paths = new[] { "/directory/file1", "/directory/directory1/file2" };

                    Assert.IsTrue(2 == directory.GetChildren().Count());
                    Assert.IsTrue(1 == directory1.GetChildren().Count());

                    fileSystem.RemoveContentFiles(paths);

                    Assert.IsTrue(1 == directory.GetChildren().Count());
                    Assert.IsTrue(!directory1.GetChildren().Any());
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryRemoveDirectoryWithRemoveContentFileFunction()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");
            var paths = new[] { "/directory" };

            fileSystem.RemoveContentFiles(paths);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryRemoveMissingContentFile()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");

            if (fileSystem.TryGetFile("/directory", out var file) && file is Directory directory)
            {
                directory.AddChild(new ContentFile("/directory/file1", "test", directory));

                var paths = new[] { "/directory/file1", "/directory/file2" };

                Assert.IsTrue(1 == directory.GetChildren().Count());

                fileSystem.RemoveContentFiles(paths);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryAddFileWithSameName()
        {
            var fileSystem = new Models.FileSystem();
            fileSystem.AddDirectory("/directory");

            if (fileSystem.TryGetFile("/directory", out var file) && file is Directory directory)
            {
                directory.AddChild(new ContentFile("/directory/file1", "test", directory));
                directory.AddChild(new ContentFile("/directory/file1", "test", directory));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
