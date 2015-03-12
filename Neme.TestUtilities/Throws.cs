using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neme.TestUtilities.Exceptions;

namespace Neme.TestUtilities
{
    public static class Throws
    {
        public static Exception Anything(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var caught = Catch.Anything(action);

            if (caught == null)
                throw new ThrowsAnythingFailed();

            return caught;
        }

        public static T Exception<T>(Action action) where T : Exception
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var caught = Catch.Anything(action);

            if (caught == null || caught.GetType() != typeof(T))
                throw new ThrowsExceptionFailed(typeof(T), caught);
     
            return (T)caught;
        }

        public static T ExceptionOrDerived<T>(Action action) where T : Exception
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var caught = Catch.Anything(action);

            if (caught == null || !(caught is T))
                throw new ThrowsExceptionOrDerivedFailed(typeof(T), caught);

            return (T)caught;
        }

        public static ArgumentNullException ArgumentNullException(Action action, string expectedParamName)
        {
            var caught = Throws.Exception<ArgumentNullException>(action);

            if (caught.ParamName != expectedParamName)
                throw new ExceptionAssertionFailed(caught, "ParamName", expectedParamName, caught.ParamName);

            return caught;
        }
    }
}
