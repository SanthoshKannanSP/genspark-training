using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_1.Factory.Interfaces;
using assignment_1.Singleton;

namespace assignment_1.Factory
{
    public abstract class MessageFactory
    {
        public abstract IMessage CreateMessage();
        private LogFileWriter _writer;

        public MessageFactory()
        {
            _writer = LogFileWriter.GetInstance();
        }

        public void Send()
        {
            IMessage message = CreateMessage();
            message.SendMessage();
            var success = _writer.WriteLog($"{message.Type} message was sent to {message.Receiver}");
            if (!success)
            {
                Console.WriteLine("Log writing failed!");
            }
        }
    }
}
