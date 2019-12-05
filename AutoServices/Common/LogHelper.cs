using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace AutoServices.Common
{
    public class LogHelper
    {
        public static readonly ILog AppLogger = LogManager.GetLogger("ColoredConsoleAppender");

        static LogHelper()
        {

        }
    }
}
