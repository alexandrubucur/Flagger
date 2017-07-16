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
    public class UserController : Controller
    {
        private readonly IUserGateway _userGateway;
        public readonly int SqlDuplicateExceptionNumber = 2601;

        public UserController(IUserGateway userGateway)
        {
            _userGateway = userGateway;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userGateway.Get();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userGateway.Get().SingleOrDefault(x => x.Id_User == id);
        }

        [HttpPost]
        public ActionResult Post([FromBody]string name)
        {
            try
            {
                _userGateway.Save(name);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                if (e.Number == SqlDuplicateExceptionNumber)
                {
                    return StatusCode(400, $"{name} user already exists");
                }
            }
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _userGateway.Delete(id);
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