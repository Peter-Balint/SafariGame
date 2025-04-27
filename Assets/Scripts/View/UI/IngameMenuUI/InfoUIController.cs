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
		private MoneyManager moneyManager;



		public TMP_Text AnimalCounter;
        public TMP_Text MoneyCounter;
        public TMP_Text HappinessCounter;
        
        void Start()
        {
            animalCollection = SafariGame.Instance.Animals;
			moneyManager = SafariGame.Instance.MoneyManager;
		}

        void Update()
        {
            AnimalCounter.text = animalCollection.Animals.Count.ToString();
			MoneyCounter.text = moneyManager.ReadBalance().ToString();

		}

    }
}
