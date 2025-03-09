using UnityEngine;
using System.Collections;
using Safari.Model;
using System.Collections.Generic;
using System;

namespace Safari.View.World
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

        private Dictionary<Type, GameObject> _mappingDictionary;

        private void OnEnable()
        {
            _mappingDictionary = new Dictionary<Type, GameObject>();
            foreach (var pair in mappings)
            {
                Type fieldType = Type.GetType(pair.FieldTypeName);
                if (fieldType != null && typeof(Field).IsAssignableFrom(fieldType))
                {
                    _mappingDictionary[fieldType] = pair.Prefab;
                }
                else
                {
                    Debug.LogError($"Invalid FieldTypeName: {pair.FieldTypeName}");
                }
            }
        }
    }
}