using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using VTTRPG.Objects;

namespace VTTRPG.Views
{
    public abstract class CharacterSheetResumeView : MonoBehaviour
    {
        public abstract void Populate(CharacterSheetObject characterSheet);
    }
}
