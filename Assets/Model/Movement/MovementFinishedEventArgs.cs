using Codice.Client.BaseCommands.BranchExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Movement
{
    public class MovementFinishedEventArgs : EventArgs
    {
        public static MovementFinishedEventArgs Success => new MovementFinishedEventArgs(new Success());

        public Result Result { get; private set; }

        public MovementFinishedEventArgs(Result result)
        {
            Result = result;
        }
    }
}
