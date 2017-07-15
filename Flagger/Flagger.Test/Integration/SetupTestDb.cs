using System;
using System.Data.SqlClient;
using System.Reflection;
using NUnit.Framework;

namespace Flagger.Test.Integration
{
    [SetUpFixture]
    public class SetupTestDb
    {
        //private static readonly TemporarySqlDatabase TemporarySqlDatabase = new TemporarySqlDatabase("FlaggerTest", @".\SQLExpress");

        //public static string ConnectionString
        //{
        //    get { return TemporarySqlDatabase.ConnectionString; }
        //}

        //[SetUp]
        //public static void SetUpFixture()
        //{
        //    TemporarySqlDatabase.Create();

        //    var upgrader =
        //        DeployChanges.To
        //            .SqlDatabase(TemporarySqlDatabase.ConnectionString)
        //            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        //            .LogToConsole()
        //            .Build();

        //    var result = upgrader.PerformUpgrade();

        //    if (!result.Successful)
        //    {
        //        throw new InvalidOperationException("Failed to run migration", result.Error);
        //    }
        //}

        //[TearDown]
        //public void TeardDownFixture()
        //{
        //    SqlConnection.ClearAllPools();
        //    TemporarySqlDatabase.Dispose();
        //}

    }
}