using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_1.Factory.Interfaces
{
    public interface IMessage
    {
        public string Content { get; set; }
        public string Type { get; }
        public string Receiver { get; set; }

        public void SendMessage();
    }
}
