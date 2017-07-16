using System.Collections.Generic;

namespace Flagger.Model
{
    public class DeleteConfiguration
    {
        public string User { get; set; }
        public IEnumerable<string> Features { get; set; }
    }
}