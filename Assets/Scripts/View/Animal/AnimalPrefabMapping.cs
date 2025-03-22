using Safari.Model.Animals;
using Safari.Model;
using System.Collections.Generic;
using System;
using UnityEngine;

#nullable enable

namespace Safari.View
{
    [CreateAssetMenu(fileName = "AnimalPrefabMapping", menuName = "Configurations/AnimalPrefabMapping")]
    public class AnimalPrefabMapping : ScriptableObject
    {
        [Serializable]
        private class AnimalPrefabPair
        {
            public string AnimalTypeName;
            public GameObject Prefab;
        }

        [SerializeField]
        private List<AnimalPrefabPair> mappings = new List<AnimalPrefabPair>();

        private Dictionary<Type, GameObject> mappingDictionary;

        public GameObject? GetPrefab(Animal animal)
        {
            return GetPrefab(animal.GetType());
        }

        public GameObject? GetPrefab(Type animalType)
        {
            if (!mappingDictionary.ContainsKey(animalType))
            {
                return null;
            }
            return mappingDictionary[animalType];
        }


        private void OnEnable()
        {
            mappingDictionary = new Dictionary<Type, GameObject>();
            foreach (var pair in mappings)
            {
                Type animalType = GetTypeByName(pair.AnimalTypeName);
                if (animalType != null && typeof(Animal).IsAssignableFrom(animalType))
                {
                    mappingDictionary[animalType] = pair.Prefab;
                }
                else
                {
                    Debug.LogError($"Invalid FieldTypeName: {pair.AnimalTypeName}");
                }
            }
        }

        private Type GetTypeByName(string typeName)
        {
            return typeof(SafariGame).Assembly.GetType("Safari.Model.Animals." + typeName);
        }
    }
}
