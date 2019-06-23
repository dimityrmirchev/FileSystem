using System;
using System.Linq;

namespace FileSystem.Models
{
    public sealed class FileSystem
    {
        private readonly Directory _root;
        private Directory _currentDirectory;

        public FileSystem()
        {
            _root = new Directory("/", null);
            _currentDirectory = _root;
        }

        public string CurrentDirectoryPath => _currentDirectory.Path;

        public void AddDirectory(string path)
        {
            AddDirectory(path, !path.StartsWith('/'));
        }

        public void ChangeDirectory(string path)
        {
            ChangeDirectory(path, !path.StartsWith('/'));
        }

        public bool TryGetDirectory(string path, out Directory directory)
        {
            return TryGetDirectory(path, !path.StartsWith('/'), out directory);
        }

        private void AddDirectory(string path, bool relative)
        {
            var lastIndex = path.LastIndexOf('/');
            if (lastIndex != -1)
            {
                var pathToAddTo = path.Substring(0, lastIndex + 1);
                var newDirectoryName = path.Substring(lastIndex + 1, path.Length - lastIndex - 1);
                if (TryGetDirectory(pathToAddTo, relative, out Directory directoryToAddTo)
                    && !TryGetDirectory(path, relative, out Directory _))
                {
                    var newDirectoryPath = directoryToAddTo.Path.EndsWith("/")
                        ? directoryToAddTo.Path + $"{newDirectoryName}"
                        : directoryToAddTo.Path + $"/{newDirectoryName}";

                    directoryToAddTo.AddChild(new Directory(newDirectoryPath, directoryToAddTo));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create directory {path}");
                }
            }
            else
            {
                if (!TryGetDirectory(path, relative, out Directory _))
                {
                    var newDirectoryPath = _currentDirectory.Path.EndsWith("/")
                        ? _currentDirectory.Path + $"{path}"
                        : _currentDirectory.Path + $"/{path}";
                    _currentDirectory.AddChild(new Directory(newDirectoryPath, _currentDirectory));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create directory {path}");
                }
            }
        }

        private void ChangeDirectory(string path, bool relative)
        {
            if (TryGetDirectory(path, relative, out Directory directory))
            {
                _currentDirectory = directory;
            }
            else
            {
                throw new InvalidOperationException($"Cannot navigate to {path}");
            }
        }

        private bool TryGetDirectory(string path, bool relative, out Directory directory)
        {
            var directoryNames = path.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            var currentDirectory = relative ? _currentDirectory : _root;
            foreach (var name in directoryNames)
            {
                if (string.Equals(name, ".."))
                {
                    currentDirectory = currentDirectory?.Parent;
                }
                else
                {
                    currentDirectory = currentDirectory?
                        .GetChildren()
                        .FirstOrDefault(c => c is Directory && string.Equals(name, c.Name)) as Directory;
                }

                if (currentDirectory == null)
                {
                    directory = null;
                    return false;
                }
            }

            directory = currentDirectory;
            return true;
        }
    }
}
