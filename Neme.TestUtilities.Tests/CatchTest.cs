using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neme.TestUtilities.Tests
{
    [TestClass]
    public class CatchTest : ExceptionTest
    {
        private void AssertCatchesAnything(Action action, Exception expected)
        {
            var caught = Catch.Anything(action);

            Assert.AreEqual(expected, caught);
        }

        private void AssertDoesNotCatchAnything(Action action)
        {
            AssertCatchesAnything(action, null);
        }
 
        private void AssertCatchesException<T>(Action action, Exception expected) where T : Exception
        {
            var caught = Catch.Exception<T>(action);

            Assert.AreEqual(expected, caught);
        }

        private void AssertDoesNotCatchException<T>(Action action, Exception actual) where T : Exception
        {
            var caught = Catch.Anything(() => { Catch.Exception<T>(action); });

            Assert.AreEqual(actual, caught);
        }

        [TestMethod]
        public void CatchAnything_NullThrows()
        {
            var caught = InnerUtilities.Catch<ArgumentNullException>(() => Catch.Anything(null));

            Assert.IsNotNull(caught);
            Assert.AreEqual("action", caught.ParamName);
        }

        [TestMethod]
        public void CatchException_NullThrows()
        {
            var caught = InnerUtilities.Catch<ArgumentNullException>(() => Catch.Exception<Exception>(null));

            Assert.IsNotNull(caught);
            Assert.AreEqual("action", caught.ParamName);
        }

        [TestMethod]
        public void CatchAnythingTest()
        {
            AssertDoesNotCatchAnything(emptyAction);
            AssertDoesNotCatchAnything(() => { try { throw new Exception(); } catch (Exception) { } });
            AssertDoesNotCatchAnything(() => { return; throw new Exception(); });

            AssertCatchesAnything(throwException, exception);
            AssertCatchesAnything(throwAssertFailedException, assertFailedException);
            AssertCatchesAnything(() => { try { throw new Exception(); } catch (Exception) { throw exception; } }, exception);
        }

        [TestMethod]
        public void CatchExceptionTest()
        {
            AssertDoesNotCatchException<Exception>(emptyAction, null);
            AssertDoesNotCatchException<AssertFailedException>(emptyAction, null);
            AssertDoesNotCatchException<ArgumentNullException>(throwArgumentException, argumentException);
            AssertCatchesException<ArgumentException>(throwArgumentNullException, argumentNullException);

            AssertCatchesException<Exception>(throwException, exception);
            AssertCatchesException<AssertFailedException>(throwAssertFailedException, assertFailedException);
        }
    }
}
