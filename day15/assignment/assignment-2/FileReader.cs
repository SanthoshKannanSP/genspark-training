using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.Models;

namespace assignment_2
{
    internal class FileReader
    {
        private ProxyFile _file;


        public FileReader(User user)
        {
            _file = new ProxyFile(user);
        }

        public void ReadData()
        {
            var fileData = _file.Read();
            if (fileData == null)
                return;
            Console.WriteLine($"File Name: {fileData.FileName}");
            Console.WriteLine($"FIle Size: {fileData.FileSize} bytes");
            Console.WriteLine($"Last Write Access: {fileData.LastWriteTime}");
            Console.WriteLine($"File Content:");
            Console.WriteLine($"{fileData.Content}");
        }
    }
}
