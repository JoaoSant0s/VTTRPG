using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VTTRPG.Objects;

namespace VTTRPG.Views
{
    public class DAndD5_0CharacterResumeView : CharacterSheetResumeView
    {
        [SerializeField]
        private TextMeshProUGUI characterNameLabel;

        public override void Populate(CharacterSheetObject characterSheet)
        {
            characterNameLabel.text = characterSheet.characterName.value;
        }
    }
}
