#nullable enable
using UnityEngine;
using System.Collections;
using Safari.Model;
using System.Collections.Generic;
using System;
using System.Linq;
using Safari.Model.Map;

namespace Safari.View.World.Map
{
    [CreateAssetMenu(fileName = "FieldPrefabMapping", menuName = "Configurations/FieldPrefabMapping")]
    public class FieldPrefabMapping : ScriptableObject
    {
        [Serializable]
        private class FieldPrefabPair
        {
            public string FieldTypeName;
            public GameObject Prefab;
        }

        [SerializeField]
        private List<FieldPrefabPair> mappings = new List<FieldPrefabPair>();

        private Dictionary<Type, GameObject> mappingDictionary;

        public GameObject? GetPrefab(Field field)
        {
            return GetPrefab(field.GetType());
        }

        public GameObject? GetPrefab(Type fieldType)
        {
            if (!mappingDictionary.ContainsKey(fieldType))
            {
                return null;
            }
            return mappingDictionary[fieldType];
        }

        private void OnEnable()
        {
            mappingDictionary = new Dictionary<Type, GameObject>();
            foreach (var pair in mappings)
            {
                Type fieldType = GetTypeByName(pair.FieldTypeName);
                if (fieldType != null && typeof(Field).IsAssignableFrom(fieldType))
                {
                    mappingDictionary[fieldType] = pair.Prefab;
                }
                else
                {
                    Debug.LogError($"Invalid FieldTypeName: {pair.FieldTypeName}");
                }
            }
        }

        private Type GetTypeByName(string typeName)
        {
            return typeof(SafariGame).Assembly.GetType("Safari.Model.Map." + typeName);
        }
    }
}