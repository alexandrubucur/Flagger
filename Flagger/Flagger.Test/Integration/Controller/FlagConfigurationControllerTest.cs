using Flagger.Controllers;
using Flagger.Core;
using Flagger.Model;
using Flagger.Test.Utils;
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

            var configuration = new ConfigurationBuilder().Build();

            mockRepo.Setup(repo => repo.Get()).Returns(new[] {configuration});

            var controller = new FlagConfigurationController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new[] { configuration });
        }

        [Fact]
        public void GetByUser()
        {
            var mockRepo = new Mock<IConfigurationGateway>();

            var feature2 = new Feature
            {
                Name = "Test2",
                Active = true
            };

            var configuration = new ConfigurationBuilder().AddFeature(feature2).Build();

            mockRepo.Setup(repo => repo.Get()).Returns(new[] { configuration });

            var controller = new FlagConfigurationController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new[] { configuration });
        }

        [Fact]
        public void Save()
        {
            var mockRepo = new Mock<IConfigurationGateway>();

            var feature2 = new Feature
            {
                Name = "Test2",
                Active = true
            };

            var configuration = new ConfigurationBuilder().AddFeature(feature2).Build();

            mockRepo.Setup(repo => repo.Get()).Returns(new[] { configuration });

            var controller = new FlagConfigurationController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new[] { configuration });
        }
    }
}