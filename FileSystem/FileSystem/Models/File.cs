using System;
using System.Linq;

namespace FileSystem.Models
{
    public abstract class File
    {
        protected File(string path)
        {
            var fileName = path.Split('/').Last().TrimEnd();
            if (fileName.Split(' ').Length != 1)
            {
                throw new ArgumentException($"Invalid file name: {fileName}");
            }
            Path = path;
            Name = fileName;
        }

        public string Path { get; }
        public string Name { get; }
    }
}
