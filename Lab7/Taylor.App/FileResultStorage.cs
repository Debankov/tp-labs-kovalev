using System;
using System.Collections.Generic;
using System.Text;
using Taylor.Core;

namespace Taylor.App
{
    public class FileResultStorage : IResultStorage
    {
        private readonly string _filePath;

        public FileResultStorage(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(string result)
        {
            File.AppendAllText(
                _filePath,
                result + Environment.NewLine

            );
        }
    }
}
