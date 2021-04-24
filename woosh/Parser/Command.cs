using System.Collections.Generic;

namespace Woosh {

    public struct Command {
        public string Name { get; set; }
        public List<string> Arguments { get; set; }
    }
}
