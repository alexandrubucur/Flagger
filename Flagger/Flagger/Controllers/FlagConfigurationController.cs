using System.Collections.Generic;
using Flagger.Core;
using Flagger.Model;
using Microsoft.AspNetCore.Mvc;

namespace Flagger.Controllers
{
    [Route("api/[controller]")]
    public class FlagConfigurationController : Controller
    {
        private readonly IConfigurationGateway _configurationGateway;

        public FlagConfigurationController(IConfigurationGateway configurationGateway)
        {
            _configurationGateway = configurationGateway;
        }

        [HttpGet]
        public IEnumerable<Configuration> Get()
        {
            return _configurationGateway.Get();
        }

        [HttpGet("{userName}")]
        public Configuration Get(string userName)
        {
            var configuration = _configurationGateway.Get(userName);

            return configuration;
        }

        [HttpPost]
        public void Post([FromBody]Configuration configuration)
        {
            _configurationGateway.Save(configuration);
        }
    }
}