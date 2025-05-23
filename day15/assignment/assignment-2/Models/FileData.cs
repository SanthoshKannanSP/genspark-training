using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2.Models
{
    internal class FileData
    {
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }

        public DateTime LastWriteTime { get; set; } = DateTime.Now;
        
        public string Content {  get; set; } = string.Empty;
    }
}
