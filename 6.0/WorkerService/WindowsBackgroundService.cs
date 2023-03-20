using WorkerService.Model.Helper;

namespace WorkerService
{
	public sealed class WindowsBackgroundService : BackgroundService
	{
		private readonly JokeService _jokeService;
		//Windows 이벤트 뷰어(Event Viewer)에 남는 로그
		private readonly ILogger<WindowsBackgroundService> _logger;

		private CancellationToken _stoppingToken;
		public WindowsBackgroundService(JokeService jokeService, ILogger<WindowsBackgroundService> logger)
		{
			_jokeService = jokeService;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				while (!_stoppingToken.IsCancellationRequested)
				{
					string joke = _jokeService.GetJoke();
					NlogHelper.LogWrite($"{joke}");
					NlogHelper.LogWrite($"Worker running at: {DateTimeOffset.Now}");
					await Task.Delay(TimeSpan.FromSeconds(1), _stoppingToken);
				}
			}
			catch (TaskCanceledException)
			{
				// When the stopping token is canceled, for example, a call made from services.msc,
				// we shouldn't exit with a non-zero exit code. In other words, this is expected...
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "{Message}", ex.Message);

				//Terminates this process and returns an exit code to the operating system.
				// This is required to avoid the 'BackgroundServiceExceptionBehavior', which
				// performs one of two scenarios:
				// 1.When set to "Ignore": will do nothing at all, errors cause zombie services.
				// 2.When set to "StopHost": will cleanly stop the host, and log errors.


				// In order for the Windows Service Management system to leverage configured
				// recovery options, we need to terminate the process with a non - zero exit code.
				Environment.Exit(1);
			}
		}
	}
}