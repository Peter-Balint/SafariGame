using Safari.View.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.World.Map
{
    public class AnimalLocationReporter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Animal"))
            {
                //var animal = other.gameObject.GetComponentInParent<AnimalDisplay>();
                //animal.Movement.ReportLocation(new GridPosition(transform.position));
            }
        }
    }
}
