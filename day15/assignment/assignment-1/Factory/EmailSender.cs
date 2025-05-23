using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_1.Factory.Interfaces;

namespace assignment_1.Factory
{
    internal class EmailSender : MessageFactory
    {
        public override IMessage CreateMessage()
        {
            var emailMessage = new EmailMessage();
            Console.Write("Enter To Mail Id: ");
            emailMessage.Receiver = Console.ReadLine();
            Console.WriteLine("Enter Message: ");
            emailMessage.Content = Console.ReadLine();
            return emailMessage;
        }
    }
}
