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
    public class UserControllerTest
    {
        [Fact]
        public void Get()
        {
            var mockRepo = new Mock<IUserGateway>();
            var user = new User
            {
                Id_User = 1,
                UserName = "Test",
                Admin = false
            };

            mockRepo.Setup(repo => repo.Get()).Returns(new[] { user });

            var controller = new UserController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new[] { user });
        }

        [Fact]
        public void Save()
        {
            var mockRepo = new Mock<IUserGateway>();

            var controller = new UserController(mockRepo.Object);

            controller.Post("test");

            mockRepo.Verify(s => s.Save("test"));
        }

        [Fact]
        public void PreventDuplicates()
        {
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601).WithErrorMessage("Duplicates key").Build();

            var mockRepo = new Mock<IUserGateway>();

            mockRepo.Setup(s => s.Save("test")).Throws(sqlException);

            var controller = new UserController(mockRepo.Object);

            var result = (ObjectResult)controller.Post("test");

            result.StatusCode.ShouldBeEquivalentTo(400);
        }
    }
}