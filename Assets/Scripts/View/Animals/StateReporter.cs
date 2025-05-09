using Safari.Model.Animals;
using Safari.View.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace Safari.View.Animals
{
    public class StateReporter : MonoBehaviour
    {
        private Animal animal;

        private string state;
        private string species;
        private Canvas canvas;
        private InfoUIController infoUIController;

		

		void Start()
        {
			
		}

	


		public void Init(Animal animal)
        {
            this.animal = animal;
			if (infoUIController == null)
			{
				infoUIController = FindFirstObjectByType<InfoUIController>();
			}

		}

   

        private void OnMouseDown()
        {
            if (animal != null)
            {
                state = animal.State.GetType().Name;
                species = animal.GetType().Name;
                infoUIController.GetCurrentAnimal(animal);
               // UnityEngine.Debug.Log($"Clicked on {animal.GetType().Name}");
               // UnityEngine.Debug.Log($"Current state:  {animal.State.GetType().Name}");

            }
        }
    }
}
