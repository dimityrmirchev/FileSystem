using System.Linq;

namespace FileSystem.Models
{
    public abstract class File
    {
        protected File(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
        public string Name => Path.Split('/').Last().TrimEnd();
    }
}
