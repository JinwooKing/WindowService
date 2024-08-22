using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace WindowService.Common
{
	class LogHelper
	{
		protected static readonly Microsoft.Extensions.Logging.ILogger logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Service1>();

		public enum LogType
		{
			Info,
			Error,
			Debug
		}

		public static void LogWrite(String msg, LogType logtype = LogType.Info)
		{
			switch (logtype)
			{
				case LogType.Info:
					logger.LogInformation(msg);
					break;
				case LogType.Error:
					logger.LogError(msg);
					break;
				case LogType.Debug:
					logger.LogDebug(msg);
					break;
				default:
					break;
			}
		}
	}
}
