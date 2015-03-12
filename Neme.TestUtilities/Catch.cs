using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neme.UnitTestUtilities
{
    public static class Catch
    {
        public static Exception Anything(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            Exception caught = null;

            try { action(); }
            catch (Exception e) { caught = e; }

            return caught;
        }

        public static Exception Exception<T>(Action action) where T : Exception
        {
            if (action == null)
                throw new ArgumentNullException("action");

            T caught = null;

            try { action(); }
            catch (T e) { caught = e; }

            return caught;
        }
    }
}
