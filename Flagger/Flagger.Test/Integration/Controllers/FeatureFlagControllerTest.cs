using Flagger.Controllers;
using Flagger.Core;
using Flagger.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Flagger.Test.Integration.Controllers
{
    [TestFixture]
    public class FeatureFlagControllerTest
    {
        [Test]
        public void Get()
        {
            var mockRepo = new Mock<IFlagGateway>();
            var flag = new Flag
            {
                Id_Flag = 1,
                Name = "Test",
                Status = false
            };

            mockRepo.Setup(repo => repo.Get()).Returns(new[] {flag});

            var controller = new FeatureFlagController(mockRepo.Object);

            var result = controller.Get();

            result.ShouldAllBeEquivalentTo(new [] {flag});
        }
    }
}
