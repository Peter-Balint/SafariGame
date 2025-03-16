using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.UI
{
    public class ConstructionUIController : MonoBehaviour
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }


        public void Close()
        {
            gameObject.SetActive(false);

        }

        public void OnDemolish()
        {

        }
    }
}
