using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neme.UnitTestUtilities.Exceptions;

namespace Neme.UnitTestUtilities.Tests.Exceptions
{
    public abstract class ThrowsCertainExceptionFailedTest
    {
        private void AssertType<T>(T e)
        {
            Assert.IsTrue(e is Exception);
            Assert.IsTrue(e is ThrowsFailed);
            Assert.IsTrue(e is ThrowsCertainExceptionFailed);
        }

        protected T GetAndAssertException<T>(Func<T> constructor, Type expectedException, Exception actualException = null) where T : ThrowsCertainExceptionFailed
        {
            var e = constructor();
            AssertType(e);

            Assert.AreEqual(expectedException, e.ExpectedException);

            if (actualException == null)
                Assert.IsNull(e.InnerException);
            else
                Assert.AreEqual(actualException, e.InnerException);

            return e;
        }
    }
}
