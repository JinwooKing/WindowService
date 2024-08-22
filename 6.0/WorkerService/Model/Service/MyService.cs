using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkerService.Model.Helper;

namespace App.WindowsService.Model.Service
{
    public class MyService
    {
        public static async Task GetVersion()
        {

            try
            {
                using (var conn = ConnectionHelper.GetConnection())
                {
                    await conn.OpenAsync();

                    var sql = "SELECT @@VERSION";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("PrintVersion: {0}", reader.GetString(0));
                            }
                        }
                    }

                    await conn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                NlogHelper.LogWrite(e.ToString(), NlogHelper.LogType.Error);
            }
        }

        public static async Task GetVersionUsingDapper()
        {
            try
            {
                using (SqlConnection conn = ConnectionHelper.GetConnection())
                {
                    await conn.OpenAsync();

                    var sql = "SELECT @@VERSION";
                    var p = new DynamicParameters();

                    var query = await conn.QueryAsync<string>(sql, p, commandType: CommandType.Text);

                    Console.WriteLine("PrintVersionUsingDapper: {0}", query.FirstOrDefault());

                    await conn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                NlogHelper.LogWrite(e.ToString(), NlogHelper.LogType.Error);
            }
        }
    }
}
