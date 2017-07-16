using System.Collections.Generic;
using Flagger.Model;

namespace Flagger.Core
{
    public interface IUserGateway
    {
        IEnumerable<User> Get();
        void Save(string userName);
        void Delete(int id);
    }
}