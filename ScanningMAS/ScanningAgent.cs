using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.Classes;
using MultiAgentSystem.Interfaces;
using Utilities;
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
        public ScanningAgent(string name,int radius, int speed,int startX,int startY)
        {
            _Radius = radius;
            _Speed = speed;
            _Name = name;
            _Position = new Point(startX, startY);
        }
        public event EventHandler<PerceptEventArgs> Percept;
        public event EventHandler<ActionEventArgs> Action;

        public void ProcessPerception(string perception)
        {
            throw new NotImplementedException();
        }

        public void StartAgent()
        {
           while(true)
            {
                EventHandler<PerceptEventArgs> handler = Percept ;
                PerceptEventArgs args = new PerceptEventArgs();
                args.Query = Utilities.Communication.ComposeSeeMessage(_Position.X, _Position.Y,_Radius);
                handler?.Invoke(this, args);

                System.Threading.Thread.Sleep(1000 / _Speed);
            }
        }

        public void StopAgent()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Format("{0}:::Radius:{1}.Speed:{2}::Position:{3},{4}", _Name, _Radius, _Speed,_Position.X,_Position.Y);
        }
        Point _Position;
        Blackboard _Blackboard;
        int _Radius;
        int _Speed;
        string _Name;
    }
}
