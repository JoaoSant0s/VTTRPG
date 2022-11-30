using System.Collections;
using System.Collections.Generic;

namespace ObjectValues.CoreValues
{
    [System.Serializable]
    public class StringValue : BaseValue<string>
    {
        public StringValue(string id, string value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
