﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neme.TestUtilities.Exceptions
{
    public class ThrowsAnythingFailed : ThrowsFailed
    {
        internal ThrowsAnythingFailed()
            : base("Expected an exception to be thrown.")
        {
        }
    }
}
