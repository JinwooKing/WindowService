namespace WorkerServiceNet8.Utils
{
    public class Consts
    {
        public static readonly string? environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        // appsettings.json 파일 로드
        private static readonly IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                        .Build();

        public static readonly string? FILE_ROOT_PATH = configuration.GetSection("Consts:FILE_ROOT_PATH").Value;
        public static readonly string? FILE_TEMP_PATH = configuration.GetSection("Consts:FILE_TEMP_PATH").Value;
    }
}
