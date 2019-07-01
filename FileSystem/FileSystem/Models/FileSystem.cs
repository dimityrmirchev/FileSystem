using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem.Models
{
    /// <summary>
    /// Provides a generic implementation of file system supporting hierarchy.
    /// </summary>
    public sealed class FileSystem
    {
        private readonly Directory _root;
        private Directory _currentDirectory;

        /// <summary>
        /// Initializes a new file system with a default root directory.
        /// </summary>
        public FileSystem()
        {
            _root = new Directory("/", null);
            _currentDirectory = _root;
        }

        /// <summary>
        /// Gets the current directory path.
        /// </summary>
        public string CurrentDirectoryPath => _currentDirectory.Path;

        /// <summary>
        /// Changes the current directory.
        /// </summary>
        /// <param name="path">The path to navigate to.</param>
        public void ChangeDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            ChangeDirectory(path, !path.StartsWith('/'));
        }

        /// <summary>
        /// Creates a new content file.
        /// </summary>
        /// <param name="path">The path to the file that is going to be created.</param>
        /// <param name="content">The content of the file that is going to be created.</param>
        public void CreateContentFile(string path, string content)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or whitespace.");
            }

            CreateContentFile(path, !path.StartsWith('/'), content);
        }

        /// <summary>
        /// Creates a new directory.
        /// </summary>
        /// <param name="path">The path to the directory that is going to be created.</param>
        public void CreateDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or whitespace.");
            }

            CreateDirectory(path, !path.StartsWith('/'));
        }

        /// <summary>
        /// Lists the children of a specific directory.
        /// </summary>
        /// <param name="path">
        /// The path to the directory that is going to be listed.
        /// If empty or whitespace the listed directory will match the current directory.
        /// </param>
        /// <returns>The children of a specific directory.</returns>
        public IEnumerable<File> ListDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return _currentDirectory.GetChildren();
            }

            if (TryGetDirectory(path, !path.StartsWith('/'), out Directory directory))
            {
                return directory.GetChildren();
            }

            throw new InvalidOperationException($"Couldn't list directory {path}");
        }

        /// <summary>
        /// Removes a list of content files.
        /// </summary>
        /// <param name="paths">The path to the files that are going to be removed.</param>
        public void RemoveContentFiles(string[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException("Value of paths cannot be null.");
            }

            var filesToRemove = new List<ContentFile>();
            foreach (var path in paths)
            {
                if (TryGetFile(path, !path.StartsWith('/'), out File file))
                {
                    if (file is ContentFile contentFile)
                    {
                        filesToRemove.Add(contentFile);
                    }
                    else
                    {
                        throw new InvalidOperationException($"{file.Path} is not a content file.");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Couldn't find file {path}");
                }
            }

            foreach (var contentFile in filesToRemove)
            {
                RemoveFile(contentFile);
            }
        }

        /// <summary>
        /// Converts a path to its file equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="file">The out value of the converted file.</param>
        /// <returns>True if successful and false if not.</returns>
        public bool TryGetFile(string path, out File file)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or whitespace.");
            }

            return TryGetFile(path, !path.StartsWith('/'), out file);
        }

        private void ChangeDirectory(string path, bool isRelative)
        {
            if (TryGetDirectory(path, isRelative, out Directory directory))
            {
                _currentDirectory = directory;
            }
            else
            {
                throw new InvalidOperationException($"Cannot navigate to {path}");
            }
        }

        private void CreateContentFile(string path, bool isRelative, string content)
        {
            var lastIndex = path.LastIndexOf('/');
            if (lastIndex != -1)
            {
                var pathToAddTo = path.Substring(0, lastIndex + 1);
                var newContentFileName = path.Substring(lastIndex + 1, path.Length - lastIndex - 1);

                if (TryGetDirectory(pathToAddTo, isRelative, out Directory directoryToAddTo) &&
                    !TryGetFile(path, isRelative, out File _))
                {
                    var newContentFilePath = GetNewFilePath(directoryToAddTo.Path, newContentFileName);
                    directoryToAddTo.AddChild(new ContentFile(newContentFilePath, content, directoryToAddTo));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create content file {path}");
                }
            }
            else
            {
                if (!TryGetFile(path, isRelative, out File _))
                {
                    var newContentFilePath = GetNewFilePath(_currentDirectory.Path, path);
                    _currentDirectory.AddChild(new ContentFile(newContentFilePath, content, _currentDirectory));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create content file {path}");
                }
            }
        }

        private void CreateDirectory(string path, bool isRelative)
        {
            var lastIndex = path.LastIndexOf('/');
            if (lastIndex != -1)
            {
                var pathToAddTo = path.Substring(0, lastIndex + 1);
                var newDirectoryName = path.Substring(lastIndex + 1, path.Length - lastIndex - 1);

                if (TryGetDirectory(pathToAddTo, isRelative, out Directory directoryToAddTo) &&
                    !TryGetFile(path, isRelative, out File _))
                {
                    var newDirectoryPath = GetNewFilePath(directoryToAddTo.Path, newDirectoryName);
                    directoryToAddTo.AddChild(new Directory(newDirectoryPath, directoryToAddTo));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create directory {path}");
                }
            }
            else
            {
                if (!TryGetFile(path, isRelative, out File _))
                {
                    var newDirectoryPath = GetNewFilePath(_currentDirectory.Path, path);
                    _currentDirectory.AddChild(new Directory(newDirectoryPath, _currentDirectory));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create directory {path}");
                }
            }
        }

        private static string GetNewFilePath(string pathToAddTo, string fileName)
        {
            return pathToAddTo.EndsWith("/")
                ? pathToAddTo + $"{fileName}"
                : pathToAddTo + $"/{fileName}";
        }

        private static void RemoveFile(File file)
        {
            if (file is ContentFile contentFile)
            {
                contentFile.Parent.RemoveChild(contentFile);
            }
            else if (file is Directory directory)
            {
                if (!directory.GetChildren().Any())
                {
                    directory.Parent.RemoveChild(directory);
                }
                else
                {
                    throw new InvalidOperationException($"{file.Path} is not an empty directory and cannot be deleted.");
                }
            }
        }

        private bool TryGetDirectory(string path, bool isRelative, out Directory directory)
        {
            if (TryGetFile(path, isRelative, out var file) && file is Directory returnDirectory)
            {
                directory = returnDirectory;
                return true;
            }

            directory = null;
            return false;
        }

        private bool TryGetFile(string path, bool isRelative, out File file)
        {
            if (string.Equals(path.Trim(), "/"))
            {
                file = _root;
                return true;
            }

            var pathToTraverse = path.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            if (pathToTraverse.Length == 0)
            {
                file = null;
                return false;
            }

            var currentDirectory = isRelative ? _currentDirectory : _root;
            for (var i = 0; i < pathToTraverse.Length - 1; i++)
            {
                if (string.Equals(pathToTraverse[i], ".."))
                {
                    currentDirectory = currentDirectory?.Parent;
                }
                else
                {
                    currentDirectory = currentDirectory?
                        .GetChildren()
                        .FirstOrDefault(c => c is Directory && string.Equals(pathToTraverse[i], c.Name)) as Directory;
                }

                if (currentDirectory == null)
                {
                    file = null;
                    return false;
                }
            }

            if (string.Equals(pathToTraverse[pathToTraverse.Length - 1].Trim(), ".."))
            {
                file = currentDirectory == _root
                    ? _root
                    : currentDirectory.Parent;
                return true;
            }

            var fileToReturn = currentDirectory?
                .GetChildren()
                .FirstOrDefault(c => string.Equals(pathToTraverse[pathToTraverse.Length - 1], c.Name));

            if (fileToReturn == null)
            {
                file = null;
                return false;
            }

            file = fileToReturn;
            return true;
        }
    }
}
