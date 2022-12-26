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

        public void ModifyValueIfNew(Color newColor)
        {
            if (this.value.Equals(newColor)) return;
            ModifyValue(newColor);
        }

        public void ModifyRedComponentValue(float redValue)
        {
            this.value.r = redValue;
        }
    }
}
