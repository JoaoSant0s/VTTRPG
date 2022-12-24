using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using VTTRPG.Views.Attachment;

namespace VTTRPG.Views
{
    public interface View
    {
        public RectTransform GetRectTransform { get; }

        public FocusViewAttachment GetFocusViewAttachment { get; }
    }
}
