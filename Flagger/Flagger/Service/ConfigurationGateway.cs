using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Flagger.Core;
using Flagger.Model;

namespace Flagger.Service
{
    public class ConfigurationGateway : IConfigurationGateway
    {
        private readonly string _connectionString;

        public ConfigurationGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Configuration> Get()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"SELECT U.UserName, F.Name, C.Active  
                                     FROM Configuration C
                                     INNER JOIN Flag F ON C.Flag_Id = F.Id_Flag
                                     INNER JOIN [User] U ON U.Id_User = C.User_Id";

                return sqlConnection.Query(sql)
                                    .GroupBy(x => new { x.UserName },
                                                  (u, f) =>
                                                      new Configuration
                                                      {
                                                          User = u.UserName,
                                                          Features = f.Select(s => new Feature { Name = s.Name, Active = s.Active })
                                                      }).ToList();
            }
        }

        public Configuration Get(string userName)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"SELECT U.UserName, F.Name, C.Active  
                                     FROM Configuration C
                                     INNER JOIN Flag F ON C.Flag_Id = F.Id_Flag
                                     INNER JOIN [User] U ON U.Id_User = C.User_Id
                                     WHERE U.UserName = @name";

                return sqlConnection.Query(sql, new { name = userName })
                                    .GroupBy(x => new { x.UserName }, 
                                                  (user, flag) =>
                                                  new Configuration
                                                  {
                                                      User = user.UserName,
                                                      Features = flag.Select(s => new Feature { Name = s.Name, Active = s.Active })
                                                  }).FirstOrDefault();
            }
        }

        public void Save(Configuration configuration)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (var transaction = sqlConnection.BeginTransaction())
                {
                    CreateTemporaryTable(configuration, sqlConnection, transaction);
                    Merge(sqlConnection, transaction);

                    transaction.Commit();
                }
            }
        }

        public void Delete(DeleteConfiguration configuration)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                const string sql = @"DELETE Configuration
                                       FROM Configuration c
                                       INNER JOIN Flag f ON c.Flag_Id = f.Id_Flag
                                       INNER JOIN [User] u ON c.User_Id = u.Id_User
                                       WHERE u.UserName = @username AND f.Name in @names";

                sqlConnection.Execute(sql, new {username = configuration.User, names = configuration.Features});

            }
        }

        private static void CreateTemporaryTable(Configuration configuration, IDbConnection sqlConnection, IDbTransaction transaction)
        {
            const string createCommand = @"CREATE TABLE #tempConfiguration(
	                                       UserName nvarchar(50),
	                                       FlagName nvarchar(50),
	                                       Active bit)";

            const string insertCommand = @"INSERT INTO #tempConfiguration (UserName, FlagName, Active) VALUES (@User, @Name, @Active)";

            sqlConnection.Execute(createCommand, null, transaction);
            sqlConnection.Execute(insertCommand, configuration.Features.Select(c => new {configuration.User, c.Name, c.Active}), transaction);
        }

        private static void Merge(IDbConnection sqlConnection, IDbTransaction transaction)
        {
            const string mergeCommand = @"WITH CTE AS
                                          (
	                                          SELECT usr.Id_User, flg.Id_Flag, tmp.Active
	                                          FROM #tempConfiguration tmp
	                                          INNER JOIN [User] usr ON tmp.UserName = usr.UserName
	                                          INNER JOIN Flag flg ON tmp.FlagName = flg.Name
                                          )
                                          MERGE INTO [Configuration] AS C
                                          USING CTE AS T
                                          ON (T.Id_User = C.User_Id and T.Id_Flag = C.Flag_Id)
                                          WHEN MATCHED
	                                          THEN UPDATE SET Active = T.Active
                                          WHEN NOT MATCHED BY TARGET
                                              THEN INSERT(Flag_Id, User_Id, Active) VALUES (T.Id_Flag, T.Id_User, T.Active);";

            sqlConnection.Execute(mergeCommand, null, transaction);
        }
    }
}