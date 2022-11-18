using System.Collections;
using System.Collections.Generic;

namespace VTTRPG.Values
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
