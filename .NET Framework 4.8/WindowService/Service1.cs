using System;
using System.ServiceProcess;
using WindowService.Common;
using WindowService.Service;

namespace WindowService
{
	public partial class Service1 : ServiceBase
	{
		ServiceName st;
		public Service1()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			LogHelper.LogWrite("WindowService started");

			try 
			{ 
				st = new ServiceName();
				st.StartService();

				LogHelper.LogWrite("StartService started");
			}
			catch (Exception e)
			{
				LogHelper.LogWrite(e.ToString(), LogHelper.LogType.Error);
			}
		}

		protected override void OnStop()
		{
			try
			{
				if(st != null)
				{
					st.StopService();
					LogHelper.LogWrite("StartService stop");
				}
			}
			catch (Exception e)
			{
				LogHelper.LogWrite(e.ToString(), LogHelper.LogType.Error);
			}

			LogHelper.LogWrite("WindowService end");
		}
	}
}
