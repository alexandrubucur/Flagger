using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            return _configurationGateway.Get(userName);
        }

        [HttpPost]
        public void Post([FromBody]Configuration configuration)
        {
            _configurationGateway.Save(configuration);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}