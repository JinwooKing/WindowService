using App.WindowsService.Model.Service;
using App.WindowsService.Model.Utils;
using App.WindowsService.Model.Worker;
using WorkerService.Model.Helper;

namespace App.WindowsService
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        
        private readonly ILogger<WindowsBackgroundService> _logger;
        private static List<Thread> thList = new List<Thread>();
        
        //private readonly JokeService _jokeService;
        //Windows �̺�Ʈ ���(Event Viewer)�� ���� �α�

        //public WindowsBackgroundService(JokeService jokeService, ILogger<WindowsBackgroundService> logger) => (_jokeService, _logger) = (jokeService, logger);
        public WindowsBackgroundService(ILogger<WindowsBackgroundService> logger) => (_logger) = (logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
            try
            {
                //if (Consts.environment == "Development")
                    //await Test();

                    //StartService();

                while (!stoppingToken.IsCancellationRequested)
                {
                    NlogHelper.LogWrite($"Worker running at: {DateTime.Now}");
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
            }
            catch (TaskCanceledException tex)
            {
                _logger.LogError(tex, "{Message}", tex.Message);
            }
            catch (Exception ex)
			{
				_logger.LogError(ex, "{Message}", ex.Message);

				Environment.Exit(1);
			}
		}

        /// <summary>
        /// �׽�Ʈ �ڵ� 
        /// launchSettings.json Environment ���� ���� ���� ����
        /// </summary>
        /// <returns></returns>
        private async Task Test()
        {
            await MyService.GetVersion();
            await MyService.GetVersionUsingDapper();
        }

        /// <summary>
        /// ���� ����
        /// </summary>
        private async void StartService()
        {
            var UserInfos = await MyService.GetUserInfos();

            foreach(var groupedUserInfos in UserInfos.GroupBy(g => g.age))
            {
                var worker = new Worker(groupedUserInfos.ToList());

                #region Thread
                var th = new Thread(new ThreadStart(worker.DoWork));
                th.Start();
                thList.Add(th);
                #endregion

                #region Timer
                //worker.Start();
                #endregion
            }
        }
    }
}