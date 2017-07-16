using Flagger.Controllers;
using Flagger.Core;
using Flagger.Model;
using Flagger.Test.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Flagger.Test.Integration.Controller
{
    public class FeatureFlagControllerTest
    {
        [Fact]
        public void Get()
        {
            var mockRepo = new Mock<IFlagGateway>();
            var flag = new Flag
            {
                Id_Flag = 1,
                Name = "Test",
                Status = false
            };

            mockRepo.Setup(repo => repo.Get()).Returns(new[] { flag });

            var controller = new FeatureFlagController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new[] { flag });
        }

        [Fact]
        public void Save()
        {
            var mockRepo = new Mock<IFlagGateway>();

            var controller = new FeatureFlagController(mockRepo.Object);

            controller.Post("test");

            mockRepo.Verify(s => s.Save("test"));
        }

        [Fact]
        public void PreventDuplicates()
        {
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(SqlExceptions.SqlDuplicateExceptionNumber).WithErrorMessage("Duplicates key").Build();

            var mockRepo = new Mock<IFlagGateway>();

            mockRepo.Setup(s => s.Save("test")).Throws(sqlException);

            var controller = new FeatureFlagController(mockRepo.Object);

            var result = (ObjectResult)controller.Post("test");

            result.StatusCode.ShouldBeEquivalentTo(400);
        }

        [Fact]
        public void Delete()
        {
            var mockRepo = new Mock<IFlagGateway>();

            var controller = new FeatureFlagController(mockRepo.Object);

            controller.Delete(1);

            mockRepo.Verify(s => s.Delete(1));
        }

        [Fact]
        public void CannotDeleteAnUsedFlag()
        {
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(SqlExceptions.SqlForeignKeyViolation).WithErrorMessage("Foreign key violation").Build();

            var mockRepo = new Mock<IFlagGateway>();

            mockRepo.Setup(s => s.Delete(1)).Throws(sqlException);

            var controller = new FeatureFlagController(mockRepo.Object);

            var result = (ObjectResult)controller.Delete(1);

            result.StatusCode.ShouldBeEquivalentTo(400);
        }
    }
}
