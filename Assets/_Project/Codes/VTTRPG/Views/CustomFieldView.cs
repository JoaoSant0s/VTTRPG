using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace VTTRPG.Views
{
    public abstract class CustomFieldView<T> : MonoBehaviour
    {
        public bool IsValidInput { get; protected set; }

        public Action OnValueUpdated;

        public abstract void AddListeners();

        public abstract void PopulateValue(T nameValue);

        protected abstract void ModifyVisual();
    }
}
