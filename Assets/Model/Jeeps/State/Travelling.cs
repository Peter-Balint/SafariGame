using Safari.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Jeeps.State
{
    public class Travelling : State
    {
        public int VisitorsOnBoard { get; private set; }

        public Travelling(Jeep owner, VisitorManager visitorManager, int visitorsOnBoard) : base(owner, visitorManager)
        {
            VisitorsOnBoard = visitorsOnBoard;
        }
    }
}
