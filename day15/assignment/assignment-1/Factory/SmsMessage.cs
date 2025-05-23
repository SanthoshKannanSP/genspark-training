using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using assignment_1.Factory.Interfaces;

namespace assignment_1.Factory
{
    internal class SmsMessage : IMessage
    {
        private string _content = string.Empty;
        private string _phonenumber = string.Empty;
        public string Content 
        { 
            get => _content;
            set => _content = value; 
        }

        public string Type => "SMS";

        public string Receiver { get => _phonenumber; set => _phonenumber = value; }

        public void SendMessage()
        {
            Console.WriteLine($"To {_phonenumber},\n{_content}");
        }
    }
}
