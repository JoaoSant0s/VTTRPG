using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Views
{
    public abstract class CustomFieldView<T> : MonoBehaviour
    {
        public bool IsValidInput { get; protected set; }

        public Action OnValueUpdated;

        protected T fieldViewValue;

        public abstract void AddListeners();

        public virtual void PopulateValue(T fieldViewValue)
        {
            this.fieldViewValue = fieldViewValue;
        }

        protected abstract void ModifyVisual();
    }
}
