using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.Interfaces;
using assignment_2.Models;

namespace assignment_2
{
    internal class File : IFile
    {
        private string _filePath = "sensitive.txt";

        public FileData? Read()
        {
            var fileData = new FileData();
            try
            {

                FileInfo fileInfo = new FileInfo(_filePath);
                fileData.FileName = fileInfo.Name;
                fileData.FileSize = fileInfo.Length;
                fileData.LastWriteTime = fileInfo.LastWriteTime;

                string contents = string.Empty;
                using (StreamReader streamReader = new StreamReader(_filePath))
                {
                    contents = streamReader.ReadToEnd();
                }

                fileData.Content = contents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                fileData = null;
            }
            return fileData;
        }
    }
}
