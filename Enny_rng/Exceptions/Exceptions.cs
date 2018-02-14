using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enny_rng.Exceptions
{

    /// <summary>
    /// Azt jelzi, hogy az átadott intervallumok nem diszjunktak (van metszetük).
    /// </summary>
    /// <seealso cref="System.Exception" />
    class NotDisjointException : Exception
    {
        
        public NotDisjointException(String msg) : base(msg){}
    }

    /// <summary>
    /// Azt jelzi, hogy az alsó határ nagyobb, mint a felső.
    /// </summary>
    /// <seealso cref="System.Exception" />
    class LowerboundMoreThanUpperboundException : Exception
    {
        public LowerboundMoreThanUpperboundException(String msg) : base(msg) { }
    }

    /// <summary>
    /// Érvénytelen valószínűség (pl: nagyobb mint 1).
    /// </summary>
    /// <seealso cref="System.Exception" />
    class InvalidPrababilityException : Exception
    {
        public InvalidPrababilityException(String msg) : base(msg) {}
    }
}
