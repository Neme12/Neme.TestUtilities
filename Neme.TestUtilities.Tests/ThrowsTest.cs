using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neme.UnitTestUtilities.Exceptions;

namespace Neme.UnitTestUtilities.Tests
{
    [TestClass]
    public class ThrowsTest : ExceptionTest
    {
        private void AssertDoesNotThrowAnything(Action action)
        {
            var caught = TestUtilities.Catch<ThrowsAnythingFailed>(() => Throws.Anything(action));

            Assert.IsNotNull(caught);
            Assert.IsNull(caught.InnerException);
        }

        private void AssertDoesNotThrowException<T>(Action action, Exception actual) where T : Exception
        {
            var caught = TestUtilities.Catch<ThrowsExceptionFailed>(() => Throws.Exception<T>(action));

            Assert.IsNotNull(caught);
            Assert.AreEqual(typeof(T), caught.ExpectedException);
            Assert.AreEqual(actual, caught.InnerException);
        }

        private void AssertDoesNotThrowArgumentNullException(Action action, Exception actual)
        {
            var caught = TestUtilities.Catch<ThrowsExceptionFailed>(() => Throws.ArgumentNullException(action, null));

            Assert.IsNotNull(caught);
            Assert.AreEqual(typeof(ArgumentNullException), caught.ExpectedException);
            Assert.AreEqual(actual, caught.InnerException);
        }

        private void AssertDoesNotThrowExceptionOrDerived<T>(Action action, Exception actual) where T : Exception
        {
            var caught = TestUtilities.Catch<ThrowsExceptionOrDerivedFailed>(() => Throws.ExceptionOrDerived<T>(action));

            Assert.IsNotNull(caught);
            Assert.AreEqual(typeof(T), caught.ExpectedException);
            Assert.AreEqual(actual, caught.InnerException);
        }

        private void AssertThrowsAnything(Action action, Exception expected)
        {
            var e = Throws.Anything(action);
            Assert.IsNotNull(e);
            Assert.AreEqual(expected, e);
        }

        private void AssertThrowsException<T>(Action action, Exception expected) where T : Exception
        {
            var e = Throws.Exception<T>(action);
            Assert.IsNotNull(e);
            Assert.AreEqual(expected, e);
        }

        private void AssertThrowsArgumentNullExceptionWithSuccess(Action action, Exception expected)
        {
            var e = Throws.ArgumentNullException(action, null);
            Assert.IsNotNull(e);
            Assert.AreEqual(expected, e);
        }

        private void AssertThrowsArgumentNullExceptionAndPropertyFails(Action action, string expectedParamName, string actualParamName, Exception expected)
        {
            var caught = TestUtilities.Catch<ExceptionAssertionFailed>(() => Throws.ArgumentNullException(action, expectedParamName));

            Assert.IsNotNull(caught);
            Assert.AreEqual(expected, caught.InnerException);
            Assert.AreEqual("ParamName", caught.PropertyName);
            Assert.AreEqual(expectedParamName, caught.ExpectedValue);
            Assert.AreEqual(actualParamName, caught.ActualValue);
        }

        private void AssertThrowsExceptionOrDerived<T>(Action action, Exception expected) where T : Exception
        {
            var e = Throws.ExceptionOrDerived<T>(action);
            Assert.IsNotNull(e);
            Assert.AreEqual(expected, e);
        }

        [TestMethod]
        public void ThrowsAnything_NullThrows()
        {
            var caught = TestUtilities.Catch<ArgumentNullException>(() => Throws.Anything(null));

            Assert.IsNotNull(caught);
            Assert.AreEqual("action", caught.ParamName);
        }

        [TestMethod]
        public void ThrowsException_NullThrows()
        {
            var caught = TestUtilities.Catch<ArgumentNullException>(() => Throws.Exception<Exception>(null));

            Assert.IsNotNull(caught);
            Assert.AreEqual("action", caught.ParamName);
        }

        [TestMethod]
        public void ThrowsExceptionOrDerived_NullThrows()
        {
            var caught = TestUtilities.Catch<ArgumentNullException>(() => Throws.ExceptionOrDerived<Exception>(null));

            Assert.IsNotNull(caught);
            Assert.AreEqual("action", caught.ParamName);
        }

        [TestMethod]
        public void ThrowsArgumentNullException_NullThrows()
        {
            var caught = TestUtilities.Catch<ArgumentNullException>(() => Throws.ArgumentNullException(null, "ParamName"));

            Assert.IsNotNull(caught);
            Assert.AreEqual("action", caught.ParamName);
        }

        [TestMethod]
        public void ThrowsAnythingTest()
        {
            AssertDoesNotThrowAnything(emptyAction);
            AssertDoesNotThrowAnything(() => { try { throw new Exception(); } catch (Exception) { } });
            AssertDoesNotThrowAnything(() => { return; throw new Exception(); });

            AssertThrowsAnything(throwException, exception);
            AssertThrowsAnything(throwAssertFailedException, assertFailedException);
            AssertThrowsAnything(() => { try { throw new Exception(); } catch (Exception) { throw exception; } }, exception);
        }

        [TestMethod]
        public void ThrowsExceptionTest()
        {
            AssertDoesNotThrowException<Exception>(emptyAction, null);
            AssertDoesNotThrowException<AssertFailedException>(emptyAction, null);
            AssertDoesNotThrowException<ArgumentNullException>(throwArgumentException, argumentException);
            AssertDoesNotThrowException<ArgumentException>(throwArgumentNullException, argumentNullException);

            AssertThrowsException<Exception>(throwException, exception);
            AssertThrowsException<AssertFailedException>(throwAssertFailedException, assertFailedException);
        }

        [TestMethod]
        public void ThrowsArgumentNullExceptionTest()
        {
            AssertDoesNotThrowArgumentNullException(emptyAction, null);
            AssertDoesNotThrowArgumentNullException(throwArgumentException, argumentException);

            AssertThrowsArgumentNullExceptionWithSuccess(throwArgumentNullException, argumentNullException);

            var withParamNameNull = new ArgumentNullException(null);
            AssertThrowsArgumentNullExceptionAndPropertyFails(() => { throw withParamNameNull; }, "", null, withParamNameNull);

            var withParamNameEmpty = new ArgumentNullException("");
            AssertThrowsArgumentNullExceptionAndPropertyFails(() => { throw withParamNameEmpty; }, null, "", withParamNameEmpty);

            var withBadParamName = new ArgumentNullException("BadParamName");
            AssertThrowsArgumentNullExceptionAndPropertyFails(() => { throw withBadParamName; }, "ParamName", "BadParamName", withBadParamName);
        }

        [TestMethod]
        public void ThrowsExceptionOrDerivedTest()
        {
            AssertDoesNotThrowExceptionOrDerived<Exception>(emptyAction, null);
            AssertDoesNotThrowExceptionOrDerived<AssertFailedException>(emptyAction, null);
            AssertDoesNotThrowExceptionOrDerived<ArgumentNullException>(throwArgumentException, argumentException);
            AssertThrowsExceptionOrDerived<ArgumentException>(throwArgumentNullException, argumentNullException);

            AssertThrowsExceptionOrDerived<Exception>(throwException, exception);
            AssertThrowsExceptionOrDerived<AssertFailedException>(throwAssertFailedException, assertFailedException);
        }
    }
}
