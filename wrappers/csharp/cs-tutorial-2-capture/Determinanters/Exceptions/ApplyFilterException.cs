using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Determinanters
{
    public class ApplyFilterException : Exception
    {
        public ApplyFilterException(Exception source) : base("Filter internal exception", source)
        { }
    }
}
