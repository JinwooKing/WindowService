using NLog.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WorkerService.Model.Helper
{
    /// <summary>
	/// 로그파일에 로그 생성
	/// </summary>
	public class NlogHelper
    {
        private static readonly ILogger logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,          // JSON 문자열 인코딩에 사용할 인코더를 설정합니다.
            WriteIndented = true,                                           // JSON을 들여쓰기하여 가독성을 높입니다.
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,   // null 값이 있는 속성을 직렬화할 때 무시하는 기본 동작을 설정합니다.
            MaxDepth = 8,                                                   // 직렬화할 수 있는 개체의 최대 깊이를 설정합니다. 너무 깊은 개체는 예외를 발생시킬 수 있습니다.
            ReadCommentHandling = JsonCommentHandling.Skip,                 // JSON 데이터에서 주석을 처리하는 방법을 설정합니다. 여기서는 주석을 건너뜁니다.
            ReferenceHandler = ReferenceHandler.Preserve                    // 개체 참조를 유지하기 위해 참조 처리기를 설정합니다. 이렇게 하면 참조 루프가 발생할 수 있습니다.
        };

        public enum LogType
        {
            Info,
            Error,
            Debug,
            Critical
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
                case LogType.Critical:
                    logger.LogCritical(msg);
                    break;
                default:
                    break;
            }
        }
        public static void Log(object obj)
        {
            LogWrite($"{obj.GetType()}: {JsonSerializer.Serialize(obj, options)}");
        }
    }
}
