using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Flagger.Core;
using Flagger.Model;

namespace Flagger.Service
{
    public class FlagGateway : IFlagGateway
    {
        private readonly string _connectionString;

        public FlagGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Flag> Get()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"SELECT Id_Flag ,Name ,Status
                                     FROM Flag";

                return sqlConnection.Query<Flag>(sql);
            }
        }

        public void Save(string name)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"INSERT Flag(Name) VALUES(@name)";

                sqlConnection.Execute(sql, new {name});
            }
        }

        public void Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"DELETE FROM Flag WHERE Id_Flag = @id";

                sqlConnection.Execute(sql, new { id });
            }
        }
    }
}
