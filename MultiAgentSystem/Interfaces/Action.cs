using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem.Interfaces
{
    public interface IAction
    {
        void execute(IAgent agent, string perception);
    }
}
