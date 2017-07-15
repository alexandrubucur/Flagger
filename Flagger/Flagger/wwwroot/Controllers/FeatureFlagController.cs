using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flagger.Core;
using Flagger.Model;
using Microsoft.AspNetCore.Mvc;

namespace Flagger.Controllers
{
    [Route("api/[controller]")]
    public class FeatureFlagController : Controller
    {
        private readonly IFlagGateway _flagGateway;

        public FeatureFlagController(IFlagGateway flagGateway)
        {
            _flagGateway = flagGateway;
        }

        [HttpGet]
        public IEnumerable<Flag> Get()
        {
            return _flagGateway.Get();
        }

        [HttpGet("{id}")]
        public Flag Get(int id)
        {
            return _flagGateway.Get().SingleOrDefault(x => x.Id_Flag == id);
        }

        [HttpPost]
        public void Post([FromBody]string name)
        {
            _flagGateway.Save(name);
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
