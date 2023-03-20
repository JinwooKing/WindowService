using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Logging;

namespace WorkerService.Model.Helper
{
    public class DBHelper
    {
		private static string SERVER = "000.000.00.000,0000";
		private static string UID = "00";
		private static string PWD = "000000";
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
		public static string GetConnectionString()
        {
			return sqlConnectionStringBuilder.ConnectionString;
        }
    }
}
