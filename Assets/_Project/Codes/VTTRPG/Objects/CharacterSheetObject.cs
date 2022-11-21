using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using VTTRPG.Values;
using VTTRPG.Assets;

namespace VTTRPG.Objects
{
    public class CharacterSheetObject
    {
        public string systemId;
        public StringValue characterName;
        public Dictionary<string, List<IntValue>> intValuesCollections;

        public CharacterSheetObject(string systemId)
        {
            var generalConfig = ResourcesWrapper.LoadGeneralConfig();
            this.systemId = systemId;
            this.characterName = new StringValue(generalConfig.characterNameKey, generalConfig.defaultCharacterName);
            this.intValuesCollections = new Dictionary<string, List<IntValue>>();
        }

        public CharacterSheetObject(CharacterSheetSave save)
        {
            this.systemId = save.systemId;
            this.characterName = save.characterName;
            this.intValuesCollections = save.intValuesCollections.ToDictionary(key => key.key, value => value.values);
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
            return $"{this.systemId} {this.intValuesCollections.Count}";
        }
    }
}
