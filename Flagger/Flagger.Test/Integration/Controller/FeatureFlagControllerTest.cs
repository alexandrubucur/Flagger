using Flagger.Controllers;
using Flagger.Core;
using Flagger.Model;
using FluentAssertions;
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
    }
}
