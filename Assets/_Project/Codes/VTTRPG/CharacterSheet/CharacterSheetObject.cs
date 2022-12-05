using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using ObjectValues.CoreValues;
using VTTRPG.CustomServices;
using JoaoSant0s.ServicePackage.General;

namespace VTTRPG.Objects
{
    public class CharacterSheetObject
    {
        public string rpgId;
        public StringValue characterName;
        public Dictionary<string, List<IntValue>> intValuesCollections;

        public CharacterSheetObject() { }

        public CharacterSheetObject(string rpgId)
        {
            var generalConfig = Services.Get<RPGContentService>().GetCharacterSheetConfig();
            this.rpgId = rpgId;
            this.characterName = new StringValue(generalConfig.characterNameKey, generalConfig.defaultCharacterName);
            this.intValuesCollections = new Dictionary<string, List<IntValue>>();
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
