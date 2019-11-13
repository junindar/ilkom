using System;
using System.Globalization;
using System.IO;
using Topshelf;


namespace LatihanWinServices
{

    class LatihanService : ServiceControl
    {
        private const string LogLocation = @"C:\temp\mylog.txt";

        private void WriteLog(string message)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(LogLocation));
            File.AppendAllText(LogLocation, 
               $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} : {message} {Environment.NewLine}");
        }

        public bool Start(HostControl hostControl)
        {
            WriteLog("Service Mulai");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            WriteLog("Service Berhenti");
            return true;
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                configurator.SetServiceName("Ilmu Komputer Service");

                configurator.SetDescription("Ini adalah latihan membuat windows service menggunakan .Net Core");
                configurator.Service<LatihanService>();
            });

            // HostFactory.Run(s => s.Service<LatihanService>());
        }
    }
}
