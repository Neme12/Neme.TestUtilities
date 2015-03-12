using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neme.TestUtilities.Exceptions
{
    public abstract class ThrowsFailed : Exception
    {
        internal ThrowsFailed(string message, Exception actualException = null)
            : base(message, actualException)
        {
        }
    }
}
