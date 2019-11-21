using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace LatihanWinServiceAndQuartz
{
   public class Log
    {

        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        public static void WriteLog( string Message)
        {

            string LogFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Logs"; ;
            if (!Directory.Exists(LogFolder)) Directory.CreateDirectory(LogFolder);

            String LogFileName = DateTime.Now.ToString("yyyy.MM.dd") + ".txt";
            LogFileName = LogFolder + @"\" + LogFileName;
            string msg = DateTime.Now + ": " + Message + Environment.NewLine;

            _readWriteLock.EnterWriteLock();
            try
            {
                using (StreamWriter sw = File.AppendText(LogFileName))
                {
                    sw.WriteLine(msg);
                    sw.Close();
                }
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }

        }


    }
}
