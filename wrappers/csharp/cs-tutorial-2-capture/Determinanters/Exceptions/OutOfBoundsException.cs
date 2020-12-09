using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Determinanters
{
    public class OutOfBoundsException : Exception
    {
        public OutOfBoundsException() : base("Selected area out of bound the frame")
        { }
    }
}
