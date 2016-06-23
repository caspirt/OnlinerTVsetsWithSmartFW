using System;
using log4net;

namespace demo.framework
{
    public class Logger
    {
        private ILog logger;
        public Logger(ILog logger)
        {
            this.logger = logger;
        }

        public void Step(int number)
        {
            logger.Info("== Step " + number + " ==");
        }

        public void Step(int number, String message)
        {
            logger.Info("== Step " + number + ": " + message);
        }

        public void Info(String info)
        {
            logger.Info(info);
        }

        public void Fatal(String fatal)
        {
            logger.Fatal(fatal);
        }
    }
}
