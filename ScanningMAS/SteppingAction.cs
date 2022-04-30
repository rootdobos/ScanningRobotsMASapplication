using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.Interfaces;
using Utilities;

namespace ScanningMAS
{
    public class SteppingAction : IAction
    {
        public SteppingAction(List<Point> agentPositions,List<Point> edgePositions, List<Point> boundaryPosition)
        {
            _agentPositions = agentPositions;
            _edgePositions = edgePositions;
            _boundaryPosition = boundaryPosition;
        }
        public void execute(IAgent agent, string perception)
        {
            Point agentPosition = ((ScanningAgent)agent).Position;
            Random random = new Random();
            int x=random.Next(3);
            int y = random.Next(3);
            Point newPos = new Point();
                newPos.X = agentPosition.X + (x - 1);
                newPos.Y = agentPosition.Y + (y - 1);
            if (!ImageProcessing.IsObject(newPos, _edgePositions, _boundaryPosition))
                ((ScanningAgent)agent).Position = newPos;

        }
        List<Point> _agentPositions;
        List<Point> _edgePositions;
        List<Point> _boundaryPosition;
    }
}
