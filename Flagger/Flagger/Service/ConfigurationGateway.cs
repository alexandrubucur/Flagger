using System;
using System.Collections.Generic;
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
                const string sql = @"SELECT U.UserName, F.Name, C.Active  FROM Configuration C
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
                const string sql = @"SELECT U.UserName, F.Name, C.Active  FROM Configuration C
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
            throw new NotImplementedException();
        }
    }
}