using System;
using System.Linq;
using FileSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Tests
{
    [TestClass]
    public class DirectoryTests
    {
        [TestMethod]
        public void CreateDirectory()
        {
            var path = "/sample";
            var directory = new Directory(path, null);

            Assert.AreEqual(directory.Path, path);
            Assert.AreEqual(directory.Name, "sample");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidDirectory()
        {
            var path = "/sample dsa";
            var directory = new Directory(path, null);
        }

        [TestMethod]
        public void ParseNameOfDirectory()
        {
            var path = "/sample/test/text.txt";
            var directory = new Directory(path, null);

            Assert.AreEqual(directory.Path, path);
            Assert.AreEqual(directory.Name, "text.txt");
        }

        [TestMethod]
        public void AddChildrenToDirectory()
        {
            var path = "/sample";
            var directory = new Directory(path, null);

            directory.AddChild(new Directory(path + "/sample1", directory));
            directory.AddChild(new ContentFile(path + "/test.txt", "Sample text."));

            var children = directory.GetChildren().ToList();

            Assert.IsTrue(children.Count(c => c is Directory) == 1);
            Assert.IsTrue(children.Count(c => c is ContentFile) == 1);
        }

        [TestMethod]
        public void RemoveChildrenFromDirectory()
        {
            var path = "/sample";
            var directory = new Directory(path, null);
            var contentFile = new ContentFile(path + "/test.txt", "Sample text.");

            directory.AddChild(new Directory(path + "/sample1", directory));
            directory.AddChild(contentFile);

            directory.RemoveChild(contentFile);

            Assert.IsTrue(directory.GetChildren().Count() == 1);
        }
    }
}
