using System;
using System.Threading;
using WindowService.Common;

namespace WindowService.Service
{
	public class ServiceName
	{
		ManualResetEvent StopThread;
		Thread th;

		/// <summary>
		/// 서비스 시작
		/// </summary>
		public void StartService()
		{
			th = new Thread(new ThreadStart(StartServiceName));
			th.Start();

			IniHelper.InitIni();
		}

		/// <summary>
		/// 서비스 종료
		/// </summary>
		public void StopService()
		{
			// terminate the simulation thread
			if (th != null)
			{
				StopThread = new ManualResetEvent(false);
				StopThread.WaitOne(10000, false);
				StopThread.Close();
				StopThread = null;
			}

			if (th.IsAlive)
				th.Abort();

		}

		public void StartServiceName()
		{
			try
			{
				for (; ; )
				{
					if (StopThread != null)
					{
						StopThread.Set();
						return;
					}

					//TODO
					LogHelper.LogWrite($"Service running at: {DateTimeOffset.Now}");
					Thread.Sleep(new TimeSpan(0, 0, 1));
				}
			}
			catch(Exception e)
			{
				LogHelper.LogWrite(e.ToString(),LogHelper.LogType.Error);
			}
		}
	}
}
