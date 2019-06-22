using System.Collections.Generic;
using System.Linq;

namespace FileSystem.Models
{
    public class Directory : File
    {
        private readonly Directory _parent;
        private readonly List<File> _children;

        public Directory(string path, Directory parent) : base(path)
        {
            _parent = parent;
            _children = new List<File>();
        }

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
