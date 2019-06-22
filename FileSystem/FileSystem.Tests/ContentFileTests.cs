using FileSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Tests
{
    [TestClass]
    public class ContentFileTests
    {
        [TestMethod]
        public void CreateContentFile()
        {
            var content = "This is a sample file.";
            var path = "/sample.txt";
            var file = new ContentFile(path, content);

            Assert.AreEqual(file.Content, content);
            Assert.AreEqual(file.Path, path);
            Assert.AreEqual(file.Name, "sample.txt");
        }
    }
}
