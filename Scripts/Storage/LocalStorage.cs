using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace SticksCandy.Services.Save
{
    public class LocalStorage : IStorage
    {
        private const string FILE_EXTENSION = "json";
        private readonly string _rootPath;
        
        private readonly Dictionary<string, DataFile> _dataFiles;

        private ISerializer<string> _serializer;

        public LocalStorage(string rootFolder = "LocalStorage")
        {
            _serializer = new JSONSerializer();
            _dataFiles = new Dictionary<string, DataFile>();

            _rootPath = Path.Combine(Application.persistentDataPath, rootFolder);

            CreateRootDirectory();
            
            Fetch();
        }
        
        public virtual async Task<TData> Get<TData>(string key) where TData : class
        {
            if (_dataFiles.TryGetValue(key, out var dataFile) == false) return default;

            try
            {
                var content = await dataFile.Read();
                var data = await _serializer.Deserialize<TData>(content);

                if (data == default) data = Activator.CreateInstance<TData>();

                return data;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Save.LocalStorage: {exception.Message}");
                return default;
            }
            
        }

        public virtual async Task Put<TData>(string key, TData data)
        {
            if (_dataFiles.TryGetValue(key, out var dataFile) == false)
            {
                var pathFile = Path.Combine(_rootPath, key);
                dataFile = FileSystem.CreatFile(pathFile, FILE_EXTENSION);
                
                _dataFiles.Add(key, dataFile);
            }

            var serializedData = await _serializer.Serialize(data);
            
            await dataFile.Write(serializedData);
        }

        public virtual async Task Delete<TData>(string key)
        {
            if (_dataFiles.TryGetValue(key, out var dataFile) == false) return;
            
            await dataFile.Delete();
            
            _dataFiles.Remove(key);
        }

        private void Fetch()
        {
            _dataFiles.Clear();

            var dataFiles = FileSystem.GetAllFilesInDirectory(_rootPath);

            for (int index = 0, count = dataFiles.Count; index < count; index++)
            {
                var dataFile = dataFiles[index];
                _dataFiles.Add(dataFile.Name, dataFile);
            }
        }

        private void CreateRootDirectory()
        {
            if (Directory.Exists(_rootPath) == false)
            {
                Directory.CreateDirectory(_rootPath);
            }
        }
    }
}