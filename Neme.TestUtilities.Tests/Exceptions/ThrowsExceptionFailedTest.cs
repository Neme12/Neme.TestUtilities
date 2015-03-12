using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neme.TestUtilities.Tests;

namespace Neme.TestUtilities.Exceptions.Tests
{
    [TestClass]
    public class ThrowsExceptionFailedTest : ThrowsCertainExceptionFailedTest
    {
        const string message = "Expected an exception of type {0} to be thrown.";
        const string messageWithActualException = "Expected an exception of type {0} to be thrown. Actual exception: {1}";

        private void CreateAndAssertException<T>() where T : Exception
        {
            var e = GetAndAssertException(() => new ThrowsExceptionFailed(typeof(T)), typeof(T));

            Assert.AreEqual(string.Format(message, typeof(T)), e.Message);
        }

        private void CreateAndAssertException<T>(Exception actualException) where T : Exception
        {
            var e = GetAndAssertException(() => new ThrowsExceptionFailed(typeof(T), actualException), typeof(T), actualException);

            string expectedMessage = actualException == null
                ? string.Format(message, typeof(T))
                : string.Format(messageWithActualException, typeof(T), actualException.GetType());

            Assert.AreEqual(expectedMessage, e.Message);
        }

        [TestMethod]
        public void CreateWithoutActualException()
        {
            CreateAndAssertException<Exception>();
            CreateAndAssertException<AssertFailedException>();
        }

        [TestMethod]
        public void CreateWithActualExceptionNull()
        {
            CreateAndAssertException<Exception>(null);
            CreateAndAssertException<AssertFailedException>(null);
        }

        [TestMethod]
        public void CreateWithActualException()
        {
            CreateAndAssertException<ArgumentException>(new ArgumentNullException());
            CreateAndAssertException<ArgumentNullException>(new ArgumentException());
        }

        [TestMethod]
        public void CreateWithExpectedExceptionNullThrows()
        {
            var e1 = InnerUtilities.Catch<ArgumentNullException>(() => { new ThrowsExceptionFailed(null); });
            Assert.IsNotNull(e1);
            Assert.AreEqual("expectedException", e1.ParamName);

            var e2 = InnerUtilities.Catch<ArgumentNullException>(() => { new ThrowsExceptionFailed(null, null); });
            Assert.IsNotNull(e2);
            Assert.AreEqual("expectedException", e2.ParamName);
        }

        [TestMethod]
        public void CreateWithWrongExpectedTypeThrows()
        {
            var e1 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionFailed(typeof(object)); });
            Assert.IsNotNull(e1);
            Assert.AreEqual("expectedException", e1.ParamName);

            var e2 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionFailed(typeof(int), null); });
            Assert.IsNotNull(e2);
            Assert.AreEqual("expectedException", e2.ParamName);
        }

        [TestMethod]
        public void CreateWithActualSameAsExpectedThrows()
        {
            var e1 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionFailed(typeof(Exception), new Exception()); });
            Assert.IsNotNull(e1);
            Assert.AreEqual("actualException", e1.ParamName);

            var e2 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionFailed(typeof(AssertFailedException), new AssertFailedException()); });
            Assert.IsNotNull(e2);
            Assert.AreEqual("actualException", e2.ParamName);
        }
    }
}
