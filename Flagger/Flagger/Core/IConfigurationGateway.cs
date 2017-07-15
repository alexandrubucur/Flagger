using System.Collections.Generic;
using Flagger.Model;

namespace Flagger.Core
{
    public interface IConfigurationGateway
    {
        IEnumerable<Configuration> Get();
        Configuration Get(string userName);
        void Save(Configuration configuration);
    }
}