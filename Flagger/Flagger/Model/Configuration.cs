using System.Collections.Generic;

namespace Flagger.Model
{
    public class Configuration
    {
        public string User { get; set; }
        public IEnumerable<Feature> Features { get; set; }
    }
}