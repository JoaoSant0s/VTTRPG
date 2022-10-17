using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using VTTRPG.Values;

namespace VTTRPG.Objects
{
    public class CharacterSheetObject
    {
        public Dictionary<string, List<IntValue>> intValuesCollections = new Dictionary<string, List<IntValue>>();

        public List<IntValue> GetOrCreateIntValues(string key)
        {
            if (!intValuesCollections.ContainsKey(key)) intValuesCollections[key] = new List<IntValue>();
            return intValuesCollections[key];
        }

        public IntValue GetOrCreateIntValue(string key, string id, int defaultValue = 10)
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
    }
}
