using Microsoft.Data.SqlClient;
using WorkerService.Model.Helper;

namespace WorkerService
{
	public sealed class WindowsBackgroundService : BackgroundService
	{
		private readonly JokeService _jokeService;
		//Windows 이벤트 뷰어(Event Viewer)에 남는 로그
		private readonly ILogger<WindowsBackgroundService> _logger;

		private Thread _Thread;

		private CancellationToken _stoppingToken;

		public WindowsBackgroundService(JokeService jokeService, ILogger<WindowsBackgroundService> logger)
		{
			_jokeService = jokeService;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			//var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
			//config["Logging:LogLevel:Default"]

			try
			{
				_stoppingToken = stoppingToken;
				_Thread = new Thread(new ThreadStart(Dowork));
				_Thread.Start();
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

		public async void Dowork()
		{
			while (!_stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");

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
								Console.WriteLine("{0}", reader.GetString(0));
							}
						}					
					}

					conn.Close();
				}

				await Task.Delay(TimeSpan.FromSeconds(1), _stoppingToken);
			}
		}
	}
}