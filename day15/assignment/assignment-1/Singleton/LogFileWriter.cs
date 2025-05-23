using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_1.Singleton
{
    internal class LogFileWriter
    {
        private static LogFileWriter _logFileWriter;
        private StreamWriter _writer;
        private string _fileName = "log.txt";
        private LogFileWriter() 
        {
            _writer = new StreamWriter(_fileName, true);
        }

        public static LogFileWriter GetInstance()
        {
            if (_logFileWriter == null)
                _logFileWriter = new LogFileWriter();
            return _logFileWriter;
        }

        public bool WriteLog(string text)
        {
            try
            {
                _writer.WriteLine(text);
                _writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            try
            {
                _writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
