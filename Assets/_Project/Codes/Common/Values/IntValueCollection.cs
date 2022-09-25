using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
{
    public class IntValueCollection : NumberCollection<IntValue, int>
    {
        public override int Sum => value.Select(v => v.value).Sum();

        public IntValueCollection()
        {
            this.value = new List<IntValue>();
        }

        public IntValueCollection(List<IntValue> value)
        {
            this.value = value;
        }
    }
}
