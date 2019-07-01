using System;
using System.Linq;

namespace FileSystem.Models
{
    /// <summary>
    /// Provides an abstraction to help implement file system hierarchy.
    /// </summary>
    public abstract class File
    {
        protected File(string path, Directory parent)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("File path cannot be null or whitespace.");
            }

            var fileName = path.Split('/').Last().TrimEnd();
            if (fileName.Split(' ').Length != 1)
            {
                throw new ArgumentException($"Invalid file name: {fileName}");
            }

            Name = fileName;
            Parent = parent;
            Path = path;
        }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The directory containing the file.
        /// </summary>
        public Directory Parent { get; }

        /// <summary>
        /// The full path to the file.
        /// </summary>
        public string Path { get; }
    }
}
