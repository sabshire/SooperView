using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SooperView
{
    public class SooperLog(string logMessage, Color? color = null)
    {
        public string Message { get; set; } = logMessage;
        public Color Color { get; set; } = color ?? Color.Black;
    }

    public class LogHelper
    {
        public static event EventHandler<SooperLog>? LogMessageEvent;

        public static void LogMessage(object sender, string message)
        {
            LogMessageEvent?.Invoke(sender, new SooperLog(message));
        }

        public static void LogMessageColor(object sender, string message, Color color)
        {
            LogMessageEvent?.Invoke(sender, new SooperLog(message, color));
        }
    }
}
