using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MtChangeLog.WebAPI.Loggers
{
    public static class LoggerFactoryExtensions
    {
        public static void AddFile(this ILoggerFactory loggerFactory) 
        {
            DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\LogFiles");
            if (!directory.Exists) 
            {
                directory.Create();
            }
            string filePath = Path.Combine(directory.FullName, $"journal-{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}.log");
            loggerFactory.AddProvider(new FileLoggerProvider(filePath));
        }
    }
}
