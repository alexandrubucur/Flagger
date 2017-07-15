using System.Collections.Generic;
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
        public void Post([FromBody]string name)
        {
            _userGateway.Save(name);
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