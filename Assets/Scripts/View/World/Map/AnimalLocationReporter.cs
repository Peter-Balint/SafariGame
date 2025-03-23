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
                //var animal = other.gameObject.GetComponent<Animal>();
                //animal.Movement.ReportLocation(new GridPosition(transform.position));
            }
        }
    }
}
