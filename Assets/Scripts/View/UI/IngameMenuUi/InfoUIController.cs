using System;
using System.Collections.Generic;
using TMPro;
using Safari.Model.Animals;
using UnityEngine;
using UnityEngine.UI;
using Safari.Model;


namespace Safari.View
{
    public class InfoUIController : MonoBehaviour
    {
        private AnimalCollection animals;

        public List<TMP_Text> counterTextList;

        
        void Start()
        {
            animals = SafariGame.Instance.Animals;
            animals.Added += (sender,animal) => OnAnimalCountChanged(1);
            animals.Removed += (sender, animal) => OnAnimalCountChanged(-1);
        }

        void Update()
        {
        
        }

        private void OnAnimalCountChanged(int change) 
        {
            int textI = int.Parse(counterTextList[1].text);
            textI += change;
            counterTextList[1].text = textI.ToString();
        }
    }
}
