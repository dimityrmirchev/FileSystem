using System;
using System.Linq;

namespace FileSystem.Models
{
    public abstract class File
    {
        protected File(string path, Directory parent)
        {
            var fileName = path.Split('/').Last().TrimEnd();
            if (fileName.Split(' ').Length != 1)
            {
                throw new ArgumentException($"Invalid file name: {fileName}");
            }

            Name = fileName;
            Parent = parent;
            Path = path;
        }

        public string Name { get; }

        public Directory Parent { get; }

        public string Path { get; }
    }
}
