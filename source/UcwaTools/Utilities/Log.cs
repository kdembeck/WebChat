using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace UcwaTools.Utilities
{
    public delegate void ApplicationLogEventHandler(string logType, string logMessage);
    class Logger
    {
        static bool INVOKE_EVENT_ON_LOG = false;

        public ApplicationLogEventHandler OnApplicationLogEvent;

        public void Log(ILog logger, LogType logtype, string message)
        {
            switch (logtype)
            {
                case LogType.Debug:
                    logger.Debug(message);
                    break;
                case LogType.Error:
                    logger.Error(message);
                    break;
                case LogType.Fatal:
                    logger.Fatal(message);
                    break;
                case LogType.Info:
                    logger.Error(message);
                    break;
                case LogType.Warn:
                    logger.Warn(message);
                    break;
                default:
                    break;
            }

            if (INVOKE_EVENT_ON_LOG)
            {
                if (OnApplicationLogEvent != null)
                    OnApplicationLogEvent.Invoke(logtype.ToString(), message);
            }
        }

        public enum LogType { Debug, Info, Warn, Error, Fatal }
    }
}
