using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultiAgentSystem.Classes;
using MultiAgentSystem.Interfaces;
using Utilities;
namespace ScanningMAS
{
    public class ScanningAgent : IAgent
    {
        public Point Position
        {
            get
            {
                return _Position;
            }
            set
            {
               string message= Communication.ComposeDeletePosition(_Position.X, _Position.Y);
                _Position = value;
                _Blackboard.SetPosition(_Name, _Position);
                InvokeAction(message);
                MarkingAction mark = new MarkingAction();
                string markPosition = Communication.ComposeMarkPixel(_Position.X, _Position.Y, "blue");
                mark.execute(this, markPosition);
            }
        }
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
        public ScanningAgent(string name,int radius, int speed,int startX,int startY, Blackboard blackboard)
        {
            _Blackboard = blackboard;
            _Radius = radius;
            _Speed = speed;
            _Name = name;
            Position = new Point(startX, startY);
            MRSE.Reset();
        }
        public event EventHandler<PerceptEventArgs> Percept;
        public event EventHandler<ActionEventArgs> Action;

        public void ProcessPerception(string perception)
        {
            MarkingAction mark = new MarkingAction();
            mark.execute(this, perception);

            SteppingAction step = new SteppingAction(_Blackboard.GetPositions(), _Blackboard.GetEdges(), _Blackboard.GetBoundaries());
            step.execute(this, "");
        }
        public void InvokeAction(string arg)
        {
            EventHandler<ActionEventArgs> handler = Action;
            ActionEventArgs args = new ActionEventArgs();
            args.ActionDescription = arg;
            handler?.Invoke(this, args);

        }
        public void StartAgent()
        {
            MRSE.Set();
            if(!_started)
                MainLoop();
        }
        private void MainLoop()
        {
            _started = true;
            while (true)
            {
                MRSE.WaitOne();
                EventHandler<PerceptEventArgs> handler = Percept;
                PerceptEventArgs args = new PerceptEventArgs();
                args.Query = Utilities.Communication.ComposeSeeMessage(_Position.X, _Position.Y, _Radius);
                handler?.Invoke(this, args);

                System.Threading.Thread.Sleep(1000 / _Speed);
            }
        }
        public void StopAgent()
        {
            MRSE.Reset();
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
        ManualResetEvent MRSE = new ManualResetEvent(true);
        bool _started = false;
    }
}
