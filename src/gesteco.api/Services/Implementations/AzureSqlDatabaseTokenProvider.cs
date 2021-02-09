using System;
using System.Data.Common;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;

namespace gesteco.api.Services.Implementations {
    public class AzureSqlDatabaseTokenProvider
    {
        public void AddAccessTokenIfNotLocal(DbConnection dbConnection)
        {
            if (dbConnection == null)
                return;

            if (!(dbConnection is SqlConnection connection))
                return;

            if (IsDatabaseLocal(connection))
                return;

            connection.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net/").Result;
        }

        private bool IsDatabaseLocal(SqlConnection sqlConnection)
        {
            // note: "localhost" might need to be replaced w/ w/e is unique to your local dev db
            if (sqlConnection.ConnectionString.Contains("localhost", StringComparison.OrdinalIgnoreCase))
                return true;

            if (sqlConnection.ConnectionString.Contains("localdb", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}