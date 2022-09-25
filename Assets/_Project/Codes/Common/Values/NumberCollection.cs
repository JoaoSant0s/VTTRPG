using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public abstract class NumberCollection<T, Q>
    {
        public List<T> value;

        public abstract Q Sum { get; }
    }
}
