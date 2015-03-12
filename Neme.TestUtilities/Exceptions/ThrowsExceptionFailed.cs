using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neme.UnitTestUtilities.Exceptions
{
    public class ThrowsExceptionFailed : ThrowsCertainExceptionFailed
    {
        internal ThrowsExceptionFailed(Type expectedException, Exception actualException = null)
            : base(expectedException, false, actualException)
        {
            if (actualException != null && actualException.GetType() == expectedException)
                throw new ArgumentException(null, "actualException");
        }
    }
}
