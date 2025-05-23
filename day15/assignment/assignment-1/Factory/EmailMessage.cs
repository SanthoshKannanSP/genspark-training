using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_1.Factory.Interfaces;

namespace assignment_1.Factory
{
    internal class EmailMessage : IMessage
    {
        private string _emailContent = string.Empty;
        private string _toEmail = string.Empty;
        public string Content 
        {
            get => _emailContent;
            set => _emailContent = value; 
        }
        public string Type { get => "Email"; }
        public string Receiver { get => _toEmail; set => _toEmail = value; }

        public void SendMessage()
        {
            Console.WriteLine($"To {_toEmail},\n{_emailContent}");
        }
    }
}
