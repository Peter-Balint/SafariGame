using Safari.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Jeeps.State
{
    public class Idling : State
    {
        public Idling(Jeep owner) : base(owner)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            if (owner.VisitorManager.VisitorsWaiting > 0)
            {
                int visitors = owner.VisitorManager.TakeVisitors(Jeep.Capacity);
                TransitionTo(new Travelling(owner, visitors));
            }
        }
    }
}
