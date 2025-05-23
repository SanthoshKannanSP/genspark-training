using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_2.Interfaces;
using assignment_2.Models;

namespace assignment_2
{
    internal class ProxyFile : IFile
    {
        private readonly File _file;
        private User _user;
        public ProxyFile(User user)
        {
            _file = new File();
            _user = user;
        }

        public FileData? Read()
        {
            Console.WriteLine($"User: {_user.Username} | Role: {_user.Role}");
            switch (_user.Role)
            {
                case UserRole.Admin:
                    Console.WriteLine("[Access Granted] Reading sensitive file content...");
                    return ReadSensitive();
                case UserRole.User:
                    Console.WriteLine("[Limited Access Granter] Reading sensitive file metadata alone....");
                    return ReadLimited();
                case UserRole.Guest:
                    Console.WriteLine("[Access Denied] You do not have permission to read this file.");
                    return null;
                default:
                    Console.WriteLine("No such user role exists.");
                    return null;
            }
        }

        private FileData? ReadSensitive()
        {
            return _file.Read();
        }

        private FileData? ReadLimited()
        {
            var fileData = _file.Read();
            if (fileData != null)
                fileData.Content = "[REDACTED]";
            return fileData;
        }
    }
}
