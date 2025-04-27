using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Chaser
    {
        private Func<Vector3> getPosition;

        private Func<Vector3> getVelocity;


        public Vector3 GetPosition()
        {
            return getPosition();
        }

        public Vector3 GetVelocity()
        {
            return getVelocity();
        }

        public Chaser(Func<Vector3> getPosition, Func<Vector3> getVelocity)
        {
            this.getPosition = getPosition;
            this.getVelocity = getVelocity;
        }
    }
}
