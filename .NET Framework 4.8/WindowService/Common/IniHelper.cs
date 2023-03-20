using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowService.Common
{
	
	public class IniHelper
	{
		
		public static void InitIni()
		{
			//string currentPath = Directory.GetCurrentDirectory();
			string currentPath = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(currentPath, "ConfigEx.ini");
			
			StringBuilder FILE_TEMP_PATH = new StringBuilder();
			StringBuilder FILE_ROOT_PATH = new StringBuilder();
			StringBuilder PORT = new StringBuilder();

			if (!File.Exists(filePath))
				File.Create(filePath);

			WritePrivateProfileString("INFO", "FILE_TEMP_PATH", @"C:\TEMP", filePath);
			WritePrivateProfileString("INFO", "FILE_ROOT_PATH", @"C:\ROOT", filePath);
			WritePrivateProfileString("INFO", "PORT", "15000", filePath);

			// GetPrivateProfileString("카테고리", "Key값", "기본값", "저장할 변수", "불러올 경로");
			GetPrivateProfileString("INFO", "FILE_TEMP_PATH", "", FILE_TEMP_PATH, 32, filePath);
			GetPrivateProfileString("INFO", "FILE_ROOT_PATH", "", FILE_ROOT_PATH, 32, filePath);
			GetPrivateProfileString("INFO", "PORT", "", PORT, 32, filePath);

#if DEBUG
			LogHelper.LogWrite($"==========InitIni==========");
			LogHelper.LogWrite($"ConfigEx.ini filePath : {filePath}");
			LogHelper.LogWrite($"FILE_TEMP_PATH : {FILE_TEMP_PATH.ToString()}");
			LogHelper.LogWrite($"FILE_TEMP_PATH : {FILE_ROOT_PATH.ToString()}");
			LogHelper.LogWrite($"PORT : {PORT.ToString()}");
#endif

			// 디렉토리 확인 및 생성.                 
			if (!Directory.Exists(FILE_TEMP_PATH.ToString()))
				Directory.CreateDirectory(FILE_TEMP_PATH.ToString());
			if (!Directory.Exists(FILE_ROOT_PATH.ToString()))
				Directory.CreateDirectory(FILE_ROOT_PATH.ToString());

			Consts.FILE_TEMP_PATH = FILE_TEMP_PATH.ToString();
			Consts.FILE_ROOT_PATH = FILE_ROOT_PATH.ToString();
			Consts.PORT = PORT.ToString();
		}

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
	}
}
