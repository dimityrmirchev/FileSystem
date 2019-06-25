﻿using System;
using System.Collections.Generic;
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

        public void CreateDirectory(string path)
        {
            CreateDirectory(path, !path.StartsWith('/'));
        }

        public void ChangeDirectory(string path)
        {
            ChangeDirectory(path, !path.StartsWith('/'));
        }

        public void CreateContentFile(string path, string content)
        {
            CreateContentFile(path, !path.StartsWith('/'), content);
        }

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

        public void RemoveContentFiles(string[] paths)
        {
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
                        throw new InvalidOperationException($"{file.Path} is not a content file");
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

        public bool TryGetFile(string path, out File file)
        {
            return TryGetFile(path, !path.StartsWith('/'), out file);
        }

        private void CreateDirectory(string path, bool isRelative)
        {
            var lastIndex = path.LastIndexOf('/');
            if (lastIndex != -1)
            {
                var pathToAddTo = path.Substring(0, lastIndex + 1);
                var newDirectoryName = path.Substring(lastIndex + 1, path.Length - lastIndex - 1);
                if (TryGetDirectory(pathToAddTo, isRelative, out Directory directoryToAddTo)
                    && !TryGetFile(path, isRelative, out File _))
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
                if (!TryGetFile(path, isRelative, out File _))
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

        private void ChangeDirectory(string path, bool isRelative)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

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
                if (TryGetDirectory(pathToAddTo, isRelative, out Directory directoryToAddTo)
                    && !TryGetFile(path, isRelative, out File _))
                {
                    var newContentFilePath = directoryToAddTo.Path.EndsWith("/")
                        ? directoryToAddTo.Path + $"{newContentFileName}"
                        : directoryToAddTo.Path + $"/{newContentFileName}";

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
                    var newDirectoryPath = _currentDirectory.Path.EndsWith("/")
                        ? _currentDirectory.Path + $"{path}"
                        : _currentDirectory.Path + $"/{path}";
                    _currentDirectory.AddChild(new ContentFile(newDirectoryPath, content, _currentDirectory));
                }
                else
                {
                    throw new InvalidOperationException($"Cannot create content file {path}");
                }
            }
        }

        private void RemoveFile(File file)
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
                    throw new InvalidOperationException($"{file.Path} is not an empty directory and cannot be deleted");
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
