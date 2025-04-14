using System;
using System.Collections.Generic;
using TMPro;
using Safari.Model.Animals;
using UnityEngine;
using UnityEngine.UI;
using Safari.Model;


namespace Safari.View.UI
{
    public class InfoUIController : MonoBehaviour
    {
        private AnimalCollection animalCollection;

        public TMP_Text AnimalCounter;
        public TMP_Text MoneyCounter;
        public TMP_Text HappinessCounter;
        
        void Start()
        {
            animalCollection = SafariGame.Instance.Animals;
        }

        void Update()
        {
            AnimalCounter.text = animalCollection.Animals.Count.ToString();
        }

    }
}
