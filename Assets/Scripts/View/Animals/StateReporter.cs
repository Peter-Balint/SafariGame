using Safari.Model.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.Animals
{
    public class StateReporter : MonoBehaviour
    {
        private Animal animal;

        public void Init(Animal animal)
        {
            this.animal = animal;
        }

        private void OnMouseDown()
        {
            if (animal != null)
            {
                UnityEngine.Debug.Log($"Clicked on {animal.GetType().Name}");
                UnityEngine.Debug.Log($"Current state:  {animal.State.GetType().Name}");
                UnityEngine.Debug.Log($"Hydration percent:  {animal.State.HydrationPercent}");
                UnityEngine.Debug.Log($"Saturation percent:  {animal.State.SaturationPercent}");

            }
        }
    }
}
