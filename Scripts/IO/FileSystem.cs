using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SticksCandy.Services.Save
{
    public static class FileSystem
    {
        public static DataFile CreatFile(string path, string extension)
        {
            path += $".{extension}";
            
            using var fileStream = File.Create(path);

            var dataFile = new DataFile(path);

            return dataFile;
        }
        
        public static List<DataFile> GetAllFilesInDirectory(string directoryPath)
        {
            var dataFiles = new List<DataFile>();
            var stack = new Stack<string>();

            stack.Push(directoryPath);

            while (stack.Count > 0)
            {
                var currentDirectory = stack.Pop();

                try
                {
                    var dataFilesInDirectory = GetFilesInDirectory(currentDirectory);
            
                    if (dataFilesInDirectory != null)
                    {
                        dataFiles.AddRange(dataFilesInDirectory);
                    }

                    var directories = Directory.GetDirectories(currentDirectory);

                    for (int index = 0, count = directories.Length; index < count; index++)
                    {
                        var subdirectory = directories[index];
                        stack.Push(subdirectory);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error accessing directory: {currentDirectory}\n{ex.Message}");
                }
            }

            return dataFiles;
        }
        
        public static List<DataFile> GetFilesInDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath) == false)
            {
                Debug.LogError($"Not found directory: {directoryPath}");
                return default;
            }
            
            var dataFiles = new List<DataFile>();
                
            var pathFiles = Directory.GetFiles(directoryPath);

            for (int index = 0, count = pathFiles.Length; index < count; index++)
            {
                var file = pathFiles[index];
                var dataFile = new DataFile(file);
                    
                dataFiles.Add(dataFile);
            }

            return dataFiles;
        }
    }
}