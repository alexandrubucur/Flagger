using Flagger.Controllers;
using Flagger.Core;
using Flagger.Model;
using FluentAssertions;
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
    }
}