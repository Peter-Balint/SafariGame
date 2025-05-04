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
        public Idling(Jeep owner, VisitorManager visitorManager) : base(owner, visitorManager)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            if (visitorManager.VisitorsWaiting > 0)
            {
                int visitors = visitorManager.TakeVisitors(Jeep.Capacity);
                TransitionTo(new Travelling(owner, visitorManager, visitors));
            }
        }
    }
}
