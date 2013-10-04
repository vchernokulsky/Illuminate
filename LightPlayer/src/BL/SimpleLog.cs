using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace Radario.Common
{
    public class SimpleLog : IDisposable
    {
        private Stream _errorStream;
        private Stream _infoStream;

        private readonly object _locker;
        private SimpleLog()
        {
            _locker = new object();
            CreateLogFile();
        }

        private void CreateLogFile()
        {
            var targetDir = String.Empty;
            try
            {
#if DEBUG
                targetDir = Directory.GetCurrentDirectory();
#else
                string databasePath = String.Empty;
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\RadarioUsher"))
                {
                    if (key != null)
                        databasePath = (string) key.GetValue("Database");
                }
                targetDir = Path.GetDirectoryName(databasePath);
#endif
            } 
            catch (Exception ex)
            {
                var msg =
                    String.Format("Ошибка получения пути к файлу лога.\nException:{0}\nMessage:{1}\nStackTrace:\n{2}",
                                  ex, ex.Message, ex.StackTrace);
                Console.WriteLine(msg);
            }

            string logDir = Path.Combine(targetDir, @"Log");
            try
            {
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);

                string errorFilePath = Path.Combine(logDir, "error.log");
                string infoFilePath = Path.Combine(logDir, "info.log");
                _errorStream = !File.Exists(errorFilePath) ? File.Create(errorFilePath) : File.Open(errorFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                _infoStream = !File.Exists(infoFilePath) ? File.Create(infoFilePath) : File.Open(infoFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
            }
            catch (Exception ex)
            {
                var msg = String.Format("Ошибка при создании файла лога.\nMessage:{0}\nStackTrace:\n{1}", ex.Message, ex.StackTrace);
                Debug.WriteLine(msg);
            }
        }

        private static SimpleLog _instance;
        public static SimpleLog Log
        {
            get { return _instance ?? (_instance = new SimpleLog()); }
        }

        public void Error(string msg)
        {
            lock (_locker)
            {
                var str = String.Format("\n----------- {0} -----------\n{1}\n-----------\n", DateTime.Now, msg);
                byte[] msgInBytes = Encoding.Default.GetBytes(str);

                if (_errorStream != null)
                {
                    _errorStream.Write(msgInBytes, 0, msgInBytes.Length);
                    _errorStream.Flush();
                }
            }
        }

        public void Error(string msg, Exception ex)
        {
            lock (_locker)
            {
                var fullMsgText = String.Format("{0}\r\nException:{1}\r\nMessage:{2}\r\nStackTrace:\r\n{3}", msg, ex, ex.Message, ex.StackTrace);

                var str = String.Format("\r\n----------- {0} -----------\r\n{1}\r\n-----------\r\n", DateTime.Now, fullMsgText);
                byte[] msgInBytes = Encoding.Default.GetBytes(str);

                if (_errorStream != null)
                {
                    _errorStream.Write(msgInBytes, 0, msgInBytes.Length);
                    _errorStream.Flush();
                }
            }
        }

        public void Info(string msg)
        {
            lock (_locker)
            {
                var str = String.Format("{0}:  {1}\n", DateTime.Now, msg);
                byte[] msgInBytes = Encoding.Default.GetBytes(str);

                if (_infoStream != null)
                {
                    _infoStream.Write(msgInBytes, 0, msgInBytes.Length);
                    _infoStream.Flush();
                }
            }
        }

        public void Dispose()
        {
            if(_errorStream!=null)
                _errorStream.Dispose();
            if(_infoStream!=null)
                _infoStream.Dispose();
        }
    }
}
