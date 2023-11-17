using System.Threading.Tasks;

namespace SticksCandy.Services.Save
{
    public class DataFile
    {
        private readonly string _filePath;
        private readonly string _fileName;
        private readonly string _fileExtension;

        public string Name => _fileName;
        public string Path => _filePath;
    
        public DataFile(string filePath)
        {
            if (System.IO.File.Exists(filePath) == false)
            {
                throw new System.IO.FileNotFoundException($"Not found file for target path: {filePath}");
            }
            
            _filePath = filePath;
            _fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            _fileExtension = System.IO.Path.GetExtension(filePath);
        }

        public async Task<string> Read()
        {
            return await System.IO.File.ReadAllTextAsync(_filePath);
        }

        public async Task Write(string content)
        {
            await System.IO.File.WriteAllTextAsync(_filePath, content);
        }

        public async Task Delete()
        {
            System.IO.File.Delete(_filePath);
            
            await Task.CompletedTask;
        }

        public bool Equals(DataFile dataFile)
        {
            return Name.Equals(dataFile.Name);
        }
    }
}