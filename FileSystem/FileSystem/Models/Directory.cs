using System.Collections.Generic;
using System.Linq;

namespace FileSystem.Models
{
    public class Directory : File
    {
        private readonly List<File> _children;

        public Directory(string path, Directory parent) : base(path)
        {
            Parent = parent;
            _children = new List<File>();
        }

        public Directory Parent { get; private set; }

        public void AddChild(File file)
        {
            _children.Add(file);
        }

        public void RemoveChild(File file)
        {
            _children.Remove(file);
        }

        public IEnumerable<File> GetChildren()
        {
            return _children;
        }
    }
}
