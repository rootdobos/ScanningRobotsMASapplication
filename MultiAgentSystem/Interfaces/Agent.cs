using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem.Interfaces
{
    public interface IAgent
    {
        void StopAgent();
        void StartAgent();
        IBlackboard Blackboard { get; set; }
        event EventHandler<Classes.PerceptEventArgs> Percept;
        event EventHandler<Classes.ActionEventArgs> Action;
        void ProcessPerception(string perception);
    }
}
