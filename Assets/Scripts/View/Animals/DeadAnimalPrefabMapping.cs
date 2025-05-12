using Safari.Model.Animals;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Safari.View.Animals
{
    [CreateAssetMenu(fileName = "DeadAnimalPrefabMapping", menuName = "Configurations/DeadAnimalPrefabMapping")]
    public class DeadAnimalPrefabMapping : ScriptableObject
    {
        [Serializable]
        private class AnimalPrefabPair
        {
            public string AnimalTypeName;
            public GameObject Prefab;
        }
        [SerializeField]
        private List<AnimalPrefabPair> mappings = new List<AnimalPrefabPair>();

        public Dictionary<string, GameObject> mappingDictionary {  get; private set; }

        private void OnEnable()
        {
            mappingDictionary = new Dictionary<string, GameObject>();
            foreach (var pair in mappings)
            {
                mappingDictionary[pair.AnimalTypeName] = pair.Prefab;
            }
        }
    }
}
