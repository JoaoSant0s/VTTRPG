using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ObjectValues.CoreValues
{
    public class ColorValue : Value<Color>
    {
        public ColorValue(string id, Color value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
