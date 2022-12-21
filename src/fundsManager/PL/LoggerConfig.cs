using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Config;

namespace PL
{
    public static class LoggerConfig
    {
        private static ILog _log = null;
        public static ILog GetLogger()
        {
            if (_log is null)
            {
                _log = LogManager.GetLogger("file");
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            }
            return _log;
            
        }


    }
}
