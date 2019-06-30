using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem.Models
{
    public sealed class Directory : File
    {
        private readonly List<File> _children;

        public Directory(string path, Directory parent) : base(path, parent)
        {
            _children = new List<File>();
        }

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

        public IEnumerable<File> GetChildren()
        {
            return _children;
        }

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
