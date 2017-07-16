using System.Linq;
using Flagger.Model;

namespace Flagger.Test.Utils
{
    public class ConfigurationBuilder
    {
        private readonly Configuration _configuration;

        public ConfigurationBuilder()
        {
            _configuration = new Configuration
            {
                User = "username",
                Features = new[]
                {
                    new Feature
                    {
                        Name = "Test",
                        Active = false
                    }
                },
            };
        }

        public Configuration Build()
        {
            return _configuration;
        }

        public ConfigurationBuilder AddFeature(Feature feature)
        {
            _configuration.Features = Enumerable.Concat(_configuration.Features, new [] { feature });
            return this;
        }
    }
}