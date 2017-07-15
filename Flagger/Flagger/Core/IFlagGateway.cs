using System.Collections.Generic;
using Flagger.Model;

namespace Flagger.Core
{
    public interface IFlagGateway
    {
        IEnumerable<Flag> Get();
        void Save(string name);
    }
}