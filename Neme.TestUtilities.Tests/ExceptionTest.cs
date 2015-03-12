using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neme.TestUtilities.Tests
{
    public abstract class ExceptionTest
    {
        protected readonly Exception exception = new Exception();
        protected readonly AssertFailedException assertFailedException;

        protected readonly ArgumentException argumentException;
        protected readonly ArgumentNullException argumentNullException;

        protected readonly Action emptyAction;

        protected readonly Action throwException;
        protected readonly Action throwAssertFailedException;

        protected readonly Action throwArgumentException;
        protected readonly Action throwArgumentNullException;

        public ExceptionTest()
        {
            exception = new Exception();
            assertFailedException = new AssertFailedException();

            argumentException = new ArgumentException();
            argumentNullException = new ArgumentNullException();

            emptyAction = () => { };

            throwException = () => { throw exception; };
            throwAssertFailedException = () => { throw assertFailedException; };

            throwArgumentException = () => { throw argumentException; };
            throwArgumentNullException = () => { throw argumentNullException; };
        }
    }
}
