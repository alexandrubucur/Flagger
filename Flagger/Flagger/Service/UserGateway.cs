using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Flagger.Core;
using Flagger.Model;

namespace Flagger.Service
{
    public class UserGateway : IUserGateway
    {
        private readonly string _connectionString;

        public UserGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> Get()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"SELECT Id_User ,UserName ,Admin
                                     FROM [User]";

                return sqlConnection.Query<User>(sql);
            }
        }

        public void Save(string userName)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"INSERT [User](UserName) VALUES(@username)";

                sqlConnection.Execute(sql, new { username = userName});
            }
        }
    }
}