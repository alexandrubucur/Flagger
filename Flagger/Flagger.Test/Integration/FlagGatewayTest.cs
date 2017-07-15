using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using NUnit.Framework;

namespace Flagger.Test.Integration
{
    [TestFixture]
    public class FlagGatewayTest
    {
        [Test]
        public void SaveAndGet()
        {
            
        }

        private static void InitializeData()
        {
            //using (var connection = new SqlConnection(SetupTestDb.ConnectionString))
            //{
            //    const string productHierarchy = @"CREATE TABLE dbo.ProductHierarchy(
	           //                                 Channel varchar(50) NOT NULL,
	           //                                 ModelId varchar(50) NOT NULL,
	           //                                 VariantId varchar(50) NOT NULL,
	           //                                 Gtin char(13) NOT NULL,
            //                                );

            //                                CREATE UNIQUE NONCLUSTERED INDEX IX_Product ON dbo.ProductHierarchy
            //                                (
	           //                                 Channel ASC,
	           //                                 ModelId ASC,
	           //                                 VariantId ASC,
	           //                                 Gtin ASC
            //                                )";

            //    connection.Execute(productHierarchy);

                //connection.Execute("INSERT INTO dbo.ProductHierarchy (Channel, ModelId, VariantId, Gtin) VALUES (@Channel, @ModelId, @VariantId, @Gtin)",
                //    new[]
                //    {
                //        new ProductHierarchy(Channel, "ModelId", "VariantId", "1111111111111"),
                //        new ProductHierarchy(Channel, "ModelId", "VariantId2", "2222222222222"),
                //        new ProductHierarchy("aaaaaaa", "ModelId", "VariantId2", "2222222222222"),
                //        new ProductHierarchy(Channel, "ModelId", "VariantId4", "3333333333333"),
                //        new ProductHierarchy("aaaaaaa", "ModelId", "VariantId4", "5555555555555")
                //    });
            //}
        }
    }
}
