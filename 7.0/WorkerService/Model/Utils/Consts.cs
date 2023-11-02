using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.WindowsService.Model.Utils
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

        // ConnectionString 가져오기
        public static readonly string? local = configuration.GetConnectionString("local");
        public static readonly string? mssql = configuration.GetConnectionString("mssql");
        public static readonly string? oracle = configuration.GetConnectionString("oracle");
        public static readonly string? mariadb = configuration.GetConnectionString("mariadb");

        public static readonly int setting = Convert.ToInt16(configuration.GetSection("Consts:setting").Value);
        public static readonly int setting2 = Convert.ToInt16(configuration.GetSection("Consts:setting2").Value);

    }
}
