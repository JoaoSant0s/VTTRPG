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

        private Color CloneColor()
        {
            return new Color(this.value.r, this.value.g, this.value.b, this.value.a);
        }

        public void ModifyRedComponentValue(float newValue)
        {
            var clonedColor = CloneColor();
            clonedColor.r = newValue;
            ModifyValue(clonedColor);
        }

        public void ModifyGreenComponentValue(float newValue)
        {
            var clonedColor = CloneColor();
            clonedColor.g = newValue;
            ModifyValue(clonedColor);
        }

        public void ModifyBlueComponentValue(float newValue)
        {
            var clonedColor = CloneColor();
            clonedColor.b = newValue;
            ModifyValue(clonedColor);
        }

        public void ModifyAlphaComponentValue(float newValue)
        {
            var clonedColor = CloneColor();
            clonedColor.a = newValue;
            ModifyValue(clonedColor);
        }
    }
}
