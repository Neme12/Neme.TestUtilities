using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neme.UnitTestUtilities.Exceptions
{
    public class ThrowsExceptionOrDerivedFailed : ThrowsCertainExceptionFailed
    {
        internal ThrowsExceptionOrDerivedFailed(Type expectedException, Exception actualException = null)
            : base(expectedException, true, actualException)
        {
            if (actualException != null && IsDerived(actualException.GetType(), expectedException))
                throw new ArgumentException(null, "actualException");
        }
    }
}
