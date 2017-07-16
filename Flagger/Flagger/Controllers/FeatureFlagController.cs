using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        public IActionResult Post([FromBody]string name)
        {
            try
            {
                _flagGateway.Save(name);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                if (e.Number == SqlExceptions.SqlDuplicateExceptionNumber)
                {
                    return StatusCode(400, $"{name} feature flag already exists");
                }
            }
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _flagGateway.Delete(id);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                if (e.Number == SqlExceptions.SqlForeignKeyViolation)
                {
                    return StatusCode(400, "Cannot delete an used feature flag.");
                }
            }
            return StatusCode(200);
        }
    }
}
