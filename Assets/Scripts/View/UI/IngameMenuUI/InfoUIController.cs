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
        private MoneyManager moneyManager;
        

        public List<TMP_Text> counterTextList;

        
        void Start()
        {
            animals = SafariGame.Instance.Animals;

            animals.Added += (sender,animal) => OnAnimalCountChanged(1);
            animals.Removed += (sender, animal) => OnAnimalCountChanged(-1);
            animals.TestSpawn();

            moneyManager = SafariGame.Instance.MoneyManager;
        }

        void Update()
        {
            counterTextList[0].text  = moneyManager.ReadBalance().ToString();
		}

        private void OnAnimalCountChanged(int change) 
        {
            int textI = int.Parse(counterTextList[1].text);
            textI += change;
            counterTextList[1].text = textI.ToString();
        }
    }
}
