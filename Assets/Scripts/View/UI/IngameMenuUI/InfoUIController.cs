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
        private Animal currentAnimal;



		public TMP_Text AnimalCounter;
        public TMP_Text MoneyCounter;
        public TMP_Text HappinessCounter;
        public TMP_Text Diverstiy;
        public TMP_Text ParkSize;
		public TMP_Text MaxTicket;
		public TMP_Text AnimalSpecies;
		public TMP_Text AnimalState;

		void Start()
        {
            animalCollection = SafariGame.Instance.Animals;
			moneyManager = SafariGame.Instance.MoneyManager;
		}

        void Update()
        {
            AnimalCounter.text = animalCollection.Animals.Count.ToString();
			MoneyCounter.text = moneyManager.ReadBalance().ToString();
            HappinessCounter.text = ((int)(moneyManager.ReadVisitDesire()*100))+" %".ToString();
            Diverstiy.text = Math.Round((moneyManager.ReadDiversity() * 100), 1).ToString() + "%";
            ParkSize.text = Math.Round((moneyManager.ReadParkSize() * 100), 1).ToString() + "%";
            MaxTicket.text = Math.Round(moneyManager.ReadMaxTicketPirce(), 1).ToString() + "$";
            if(currentAnimal != null)
            {
				AnimalState.text = currentAnimal.State.GetType().Name;
				AnimalSpecies.text = currentAnimal.GetType().Name;
			}

            

		}

   
		

        public void GetCurrentAnimal(Animal animal)
        {
            currentAnimal = animal;
        }

    }
}
