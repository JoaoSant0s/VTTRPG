using System.Linq;
using System.Collections;
using System.Collections.Generic;

using ObjectValues.CoreValues;

namespace ObjectValues.CollectionValues
{
    public class FloatValueCollection : NumberCollection<FloatValue, float>
    {
        public override float Sum => value.Select(v => v.value).Sum();

        public FloatValueCollection()
        {
            this.value = new List<FloatValue>();
        }

        public FloatValueCollection(List<FloatValue> value)
        {
            this.value = value;
        }
    }
}
