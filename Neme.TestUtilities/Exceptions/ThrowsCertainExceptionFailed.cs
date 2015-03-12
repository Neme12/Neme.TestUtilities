using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Neme.UnitTestUtilities.Exceptions
{
    public abstract class ThrowsCertainExceptionFailed : ThrowsFailed
    {
        private readonly Type expectedException;
        private const string defaultMessage = "Expected an exception of type {0} to be thrown.";
        private const string derivedAllowedMessage = "Expected an exception of type {0} or a derived type to be thrown.";

        internal ThrowsCertainExceptionFailed(Type expectedException, bool derivedAllowed, Exception actualException = null)
            : base(GetMessage(expectedException, actualException, derivedAllowed), actualException)
        {
            if (expectedException == null)
                throw new ArgumentNullException("expectedException");

            if (!IsExceptionType(expectedException))
                throw new ArgumentException(null, "expectedException");

            this.expectedException = expectedException;
        }

        private static string GetMessage(Type expectedException, Exception actualException, bool derivedAllowed)
        {
            string message = string.Format(derivedAllowed ? derivedAllowedMessage : defaultMessage, expectedException);

            if (actualException != null)
                message += " Actual exception: " + actualException.GetType();

            return message;
        }

        protected static bool IsDerived(Type type, Type from)
        {
            return type == from || type.GetTypeInfo().IsSubclassOf(from);
        }

        private static bool IsExceptionType(Type type)
        {
            return IsDerived(type, typeof(Exception));
        }

        public Type ExpectedException
        {
            get { return expectedException; }
        }
    }
}
