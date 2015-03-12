using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neme.TestUtilities.Tests;

namespace Neme.TestUtilities.Exceptions.Tests
{
    [TestClass]
    public class ThrowsExceptionOrDerivedFailedTest : ThrowsCertainExceptionFailedTest
    {
        const string message = "Expected an exception of type {0} or a derived type to be thrown.";
        const string messageWithActualException = "Expected an exception of type {0} or a derived type to be thrown. Actual exception: {1}";

        private void CreateAndAssertExceptionOrDerived<T>() where T : Exception
        {
            var e = GetAndAssertException(() => new ThrowsExceptionOrDerivedFailed(typeof(T)), typeof(T));

            Assert.AreEqual(string.Format(message, typeof(T)), e.Message);
        }

        private void CreateAndAssertExceptionOrDerived<T>(Exception actualException) where T : Exception
        {
            var e = GetAndAssertException(() => new ThrowsExceptionOrDerivedFailed(typeof(T), actualException), typeof(T), actualException);

            string expectedMessage = actualException == null
                ? string.Format(message, typeof(T))
                : string.Format(messageWithActualException, typeof(T), actualException.GetType());

            Assert.AreEqual(expectedMessage, e.Message);
        }

        [TestMethod]
        public void CreateWithoutActualException()
        {
            CreateAndAssertExceptionOrDerived<Exception>();
            CreateAndAssertExceptionOrDerived<AssertFailedException>();
        }

        [TestMethod]
        public void CreateWithActualExceptionNull()
        {
            CreateAndAssertExceptionOrDerived<Exception>(null);
            CreateAndAssertExceptionOrDerived<AssertFailedException>(null);
        }

        [TestMethod]
        public void CreateWithActualException()
        {
            CreateAndAssertExceptionOrDerived<AssertFailedException>(new ArgumentException());
            CreateAndAssertExceptionOrDerived<ArgumentNullException>(new ArgumentException());
        }

        [TestMethod]
        public void CreateWithExpectedExceptionNullThrows()
        {
            var e1 = InnerUtilities.Catch<ArgumentNullException>(() => { new ThrowsExceptionOrDerivedFailed(null); });
            Assert.IsNotNull(e1);
            Assert.AreEqual("expectedException", e1.ParamName);

            var e2 = InnerUtilities.Catch<ArgumentNullException>(() => { new ThrowsExceptionOrDerivedFailed(null, null); });
            Assert.IsNotNull(e2);
            Assert.AreEqual("expectedException", e2.ParamName);
        }

        [TestMethod]
        public void CreateWithWrongExpectedTypeThrows()
        {
            var e1 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionOrDerivedFailed(typeof(object)); });
            Assert.IsNotNull(e1);
            Assert.AreEqual("expectedException", e1.ParamName);

            var e2 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionOrDerivedFailed(typeof(int), null); });
            Assert.IsNotNull(e2);
            Assert.AreEqual("expectedException", e2.ParamName);
        }

        [TestMethod]
        public void CreateWithActualSameAsExpectedThrows()
        {
            var e1 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionOrDerivedFailed(typeof(Exception), new Exception()); });
            Assert.IsNotNull(e1);
            Assert.AreEqual("actualException", e1.ParamName);

            var e2 = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionOrDerivedFailed(typeof(AssertFailedException), new AssertFailedException()); });
            Assert.IsNotNull(e2);
            Assert.AreEqual("actualException", e2.ParamName);
        }

        [TestMethod]
        public void CreateWithActualDerivedFromExpectedThrows()
        {
            var e = InnerUtilities.Catch<ArgumentException>(() => { new ThrowsExceptionOrDerivedFailed(typeof(ArgumentException), new ArgumentOutOfRangeException()); });
            Assert.IsNotNull(e);
        }
    }
}
