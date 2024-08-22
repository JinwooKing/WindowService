using App.WindowsService.Model.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Logging;
using System.Runtime.CompilerServices;

namespace WorkerService.Model.Helper
{
    public class ConnectionHelper
    {
        private static string SERVER = "000.000.000.000,0000";
		private static string UID = "00";
		private static string PWD = "00";
		private static string DATABASE = "AdventureWorksLT2019";

		private static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder
		{
			DataSource = SERVER,
			UserID = UID,
			Password = PWD,
			InitialCatalog = DATABASE,
			Encrypt = true,
			TrustServerCertificate = true,
			ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled
		};

		/// <summary>
		///  SqlConnection 객체를 반환
		/// </summary>
		/// <returns></returns>
		private static string GetConnectionString()
        {
			//return App.WindowsService.Model.Utils.Consts.mssql;
			//return sqlConnectionStringBuilder.ConnectionString;
            return Consts.local ?? sqlConnectionStringBuilder.ConnectionString;
        }

		public static SqlConnection GetConnection()
		{
			return new SqlConnection(GetConnectionString());
		}
    }
}
