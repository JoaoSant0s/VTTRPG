using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomServices;
using ObjectValues.CoreValues;

namespace VTTRPG.Objects
{
    public class CharacterSheetObject
    {
        public string rpgId;

        #region Character Sheet Header Configs

        public StringValue characterName;
        public StringValue characterResume;
        public ColorValue sheetColor;

        #endregion

        #region Character Sheet Body Configs

        public Dictionary<string, List<IntValue>> intValuesCollections;

        #endregion

        public CharacterSheetObject()
        {
            var generalConfig = Services.Get<RPGContentService>().GetCharacterSheetConfig();

            this.characterName = new StringValue(generalConfig.characterNameKey, generalConfig.defaultCharacterName);
            this.characterResume = new StringValue(generalConfig.characterResumeKey, generalConfig.defaultCharacterResume);
            this.sheetColor = new ColorValue(generalConfig.sheetColorKey, generalConfig.defaultSheetColor);
            this.intValuesCollections = new Dictionary<string, List<IntValue>>();
        }

        public CharacterSheetObject(string rpgId) : this()
        {
            this.rpgId = rpgId;
        }

        public List<IntValue> GetOrCreateIntValues(string key)
        {
            if (!intValuesCollections.ContainsKey(key)) intValuesCollections[key] = new List<IntValue>();
            return intValuesCollections[key];
        }

        public IntValue GetOrCreateIntValue(string key, string id, int defaultValue)
        {
            var attributeValues = GetOrCreateIntValues(key);

            var value = attributeValues.Find(attribute => attribute.id == id);

            if (value == null)
            {
                value = new IntValue(id, defaultValue);
                attributeValues.Add(value);
            }

            return value;
        }

        public override string ToString()
        {
            return $"{this.rpgId} {this.intValuesCollections.Count}";
        }
    }
}
