using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.Classes;
using MultiAgentSystem.Interfaces;
namespace ScanningMAS
{
    public class ScanningAgent : IAgent
    {
        public IBlackboard Blackboard
        {
            get
            {
                return _Blackboard;
            }
            set
            {
                _Blackboard = (Blackboard)value;
            }
        }
        public ScanningAgent(int radius, int speed)
        {
            _Radius = radius;
            _Speed = speed;
        }
        public event EventHandler<PerceptEventArgs> Percept;
        public event EventHandler<ActionEventArgs> Action;

        public void ProcessPerception(string perception)
        {
            throw new NotImplementedException();
        }

        public void StartAgent()
        {
            throw new NotImplementedException();
        }

        public void StopAgent()
        {
            throw new NotImplementedException();
        }

        Blackboard _Blackboard;
        int _Radius;
        int _Speed;
    }
}
