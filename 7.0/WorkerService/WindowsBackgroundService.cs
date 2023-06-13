using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkerService.Model.Helper;

namespace App.WindowsService
{
	public sealed class WindowsBackgroundService : BackgroundService
	{
		private readonly JokeService _jokeService;
		//Windows 이벤트 뷰어(Event Viewer)에 남는 로그
		private readonly ILogger<WindowsBackgroundService> _logger;

		public WindowsBackgroundService(JokeService jokeService, ILogger<WindowsBackgroundService> logger) => (_jokeService, _logger) = (jokeService, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");
                    string joke = _jokeService.GetJoke();
                    _logger.LogInformation("{Joke}", joke);
					                    
                    PrintVersionUsingDapper();
					PrintVersion();

                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
            }
			catch (TaskCanceledException)
			{

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "{Message}", ex.Message);

				Environment.Exit(1);
			}
		}

		public void PrintVersion()
		{
			using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
			{
				conn.Open();

				string sql = "SELECT @@VERSION";

				using (SqlCommand command = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Console.WriteLine("PrintVersion: {0}", reader.GetString(0));
						}
					}
				}

				conn.Close();
			}
		}

		public async void PrintVersionUsingDapper()
		{
			using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
			{
				await conn.OpenAsync();

				string sql = "SELECT @@VERSION";
				var p = new DynamicParameters();

                var query = await conn.QueryAsync<string>(sql, p, commandType: CommandType.Text);

                Console.WriteLine("PrintVersionUsingDapper: {0}", query.FirstOrDefault());
			}
		}
	}
}