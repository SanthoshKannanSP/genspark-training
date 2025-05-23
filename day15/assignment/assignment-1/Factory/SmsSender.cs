using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_1.Factory.Interfaces;

namespace assignment_1.Factory
{
    internal class SmsSender : MessageFactory
    {
        public override IMessage CreateMessage()
        {
            var smsMessage = new SmsMessage();
            Console.Write("Enter Phone number: ");
            smsMessage.Receiver = Console.ReadLine();
            Console.WriteLine("Enter Message: ");
            smsMessage.Content = Console.ReadLine();
            return smsMessage;
        }
    }
}
