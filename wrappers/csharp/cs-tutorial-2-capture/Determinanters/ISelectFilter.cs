using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Determinanters
{
    public interface ISelectFilter<T>
    {
        IEnumerable<T> Apply(IEnumerable<T> origin);
    }
}
