using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem.Models
{
    /// <summary>
    /// Represents a directory in a file system.
    /// </summary>
    public sealed class Directory : File
    {
        private readonly List<File> _children;

        /// <summary>
        /// Initializes a directory.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <param name="parent">The directory's parent.</param>
        public Directory(string path, Directory parent) : base(path, parent)
        {
            _children = new List<File>();
        }

        /// <summary>
        /// Adds a file in the directory.
        /// </summary>
        /// <param name="file">The file to be added.</param>
        public void AddChild(File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File cannot be null.");
            }

            if (_children.FirstOrDefault(c => c.Path == file.Path) != null)
            {
                throw new ArgumentException($"File {file.Path} already exists in the directory");
            }

            if (!_children.Contains(file))
            {
                _children.Add(file);
            }
        }

        /// <summary>
        /// Gets all the children on the directory.
        /// </summary>
        /// <returns>A collection of children files.</returns>
        public IEnumerable<File> GetChildren()
        {
            return _children;
        }

        /// <summary>
        /// Removes a file from the directory.
        /// </summary>
        /// <param name="file">The file to be removed.</param>
        public void RemoveChild(File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File cannot be null.");
            }

            if (_children.Contains(file))
            {
                _children.Remove(file);
            }
        }
    }
}
