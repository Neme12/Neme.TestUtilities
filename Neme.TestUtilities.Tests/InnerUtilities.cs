using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neme.TestUtilities.Tests
{
    static class InnerUtilities
    {
        public static T Catch<T>(Action action) where T : Exception
        {
            T caught = null;

            try { action(); }
            catch (T e) { caught = e; }

            return caught;
        }
    }
}
