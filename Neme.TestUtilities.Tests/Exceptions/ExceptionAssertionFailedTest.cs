using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neme.TestUtilities.Tests;

namespace Neme.TestUtilities.Exceptions.Tests
{
    [TestClass]
    public class ExceptionAssertionFailedTest
    {
        readonly Exception exception = new Exception();
        const string property = "Property";
        const string expectedValue = "ExpectedValue";
        const string actualValue = "ActualValue";

        [TestMethod]
        public void ExceptionNullThrows()
        {
            var caught = InnerUtilities.Catch<ArgumentNullException>(() =>
                new ExceptionAssertionFailed(null, property, expectedValue, actualValue)
            );

            Assert.IsNotNull(caught);
            Assert.AreEqual("exception", caught.ParamName);
        }

        [TestMethod]
        public void PropertyNullThrows()
        {
            var caught = InnerUtilities.Catch<ArgumentNullException>(() =>
                new ExceptionAssertionFailed(exception, null, expectedValue, actualValue)
            );

            Assert.IsNotNull(caught);
            Assert.AreEqual("propertyName", caught.ParamName);
        }

        [TestMethod]
        public void PropertyEmptyThrows()
        {
            var e1 = InnerUtilities.Catch<ArgumentException>(() =>
                new ExceptionAssertionFailed(exception, "", expectedValue, actualValue)
            );

            Assert.IsNotNull(e1);
            Assert.AreEqual("propertyName", e1.ParamName);

            var e2 = InnerUtilities.Catch<ArgumentException>(() =>
                new ExceptionAssertionFailed(exception, " \t\n ", expectedValue, actualValue)
            );

            Assert.IsNotNull(e2);
            Assert.AreEqual("propertyName", e2.ParamName);
        }

        [TestMethod]
        public void ValuesCannotBeSame()
        {
            var caught = InnerUtilities.Catch<ArgumentException>(() =>
                new ExceptionAssertionFailed(exception, property, expectedValue, expectedValue)
            );

            Assert.IsNotNull(caught);
            Assert.AreEqual("actualValue", caught.ParamName);
        }

        [TestMethod]
        public void ValuesCanBeNull()
        {
            new ExceptionAssertionFailed(exception, property, null, "");
            new ExceptionAssertionFailed(exception, property, "", null);
        }

        [TestMethod]
        public void TestPropertiesAndMessage()
        {
            var e = new ExceptionAssertionFailed(exception, property, expectedValue, actualValue);

            Assert.AreEqual(exception, e.InnerException);
            Assert.AreEqual(property, e.PropertyName);
            Assert.AreEqual(expectedValue, e.ExpectedValue);
            Assert.AreEqual(actualValue, e.ActualValue);

            string message = string.Format("The property {0} was expected to have the value of {1}. Actual value: {2} Exception: {3}",
                property, expectedValue, actualValue, exception);

            Assert.AreEqual(message, e.Message);
        }
    }
}
