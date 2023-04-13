using NLog.Extensions.Logging;

namespace WorkerService.Model.Helper
{
	/// <summary>
	/// 로그파일에 로그 생성
	/// </summary>
	public class NlogHelper
	{
		public static readonly Microsoft.Extensions.Logging.ILogger logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
		public enum LogType
		{
			Info,
			Error,
			Debug
		}

		public static void LogWrite(String msg, LogType logtype = LogType.Info)
		{
			Console.WriteLine(msg);

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
