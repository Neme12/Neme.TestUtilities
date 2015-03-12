using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neme.UnitTestUtilities;
using Neme.UnitTestUtilities.Exceptions;

namespace Neme.UnitTestUtilities.Tests.Exceptions
{
    [TestClass]
    public class ThrowsAnythingFailedTest
    {
        private void AssertType<T>(T e)
        {
            Assert.IsTrue(e is Exception);
            Assert.IsTrue(e is ThrowsFailed);
        }

        [TestMethod]
        public void CreateOne()
        {
            var e = new ThrowsAnythingFailed();
            AssertType(e);

            Assert.IsNull(e.InnerException);
            Assert.AreEqual("Expected an exception to be thrown.", e.Message);
        }
    }
}
