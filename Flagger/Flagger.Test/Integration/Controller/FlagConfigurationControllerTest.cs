using Flagger.Controllers;
using Flagger.Core;
using Flagger.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace Flagger.Test.Integration.Controller
{
    public class FlagConfigurationControllerTest
    {
        [Fact]
        public void GetAll()
        {
            var mockRepo = new Mock<IConfigurationGateway>();

            var feature = new Feature
            {
                Name = "Test",
                Active = false
            };

            var configuration = new Configuration
            {
                User = "username",
                Features = new [] { feature }
            };

            mockRepo.Setup(repo => repo.Get()).Returns(new[] {configuration});

            var controller = new FlagConfigurationController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new[] { configuration });
        }

        [Fact]
        public void GetByUser()
        {
            var mockRepo = new Mock<IConfigurationGateway>();

            var feature = new Feature
            {
                Name = "Test",
                Active = false
            };

            var feature2 = new Feature
            {
                Name = "Test2",
                Active = true
            };

            var configuration = new Configuration
            {
                User = "username",
                Features = new[] { feature , feature2 }
            };

            mockRepo.Setup(repo => repo.Get()).Returns(new[] { configuration });

            var controller = new FlagConfigurationController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new[] { configuration });
        }
    }
}