using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neme.TestUtilities.Exceptions
{
    public class ExceptionAssertionFailed : Exception
    {
        private readonly string propertyName;
        private readonly object expectedValue;
        private readonly object actualValue;

        internal ExceptionAssertionFailed(Exception exception, string propertyName, object expectedValue, object actualValue)
            : base(GetMessage(exception, propertyName, expectedValue, actualValue), exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException(null, "propertyName");

            if (object.Equals(expectedValue, actualValue))
                throw new ArgumentException(null, "actualValue");

            this.propertyName = propertyName;
            this.expectedValue = expectedValue;
            this.actualValue = actualValue;
        }

        private static string GetMessage(Exception exception, string propertyName, object expectedValue, object actualValue)
        {
            return string.Format("The property {0} was expected to have the value of {1}. Actual value: {2} Exception: {3}",
                propertyName, expectedValue, actualValue, exception);
        }

        public string PropertyName
        {
            get { return propertyName; }
        }

        public object ExpectedValue
        {
            get { return expectedValue; }
        }

        public object ActualValue
        {
            get { return actualValue; }
        }
    }
}
