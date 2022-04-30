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
    public class MarkingAction : IAction
    {
        public void execute(IAgent agent, string perception)
        {
            string[] pointsInformation = perception.Split('|');
            foreach (string info in pointsInformation)
            {
                string[] splitted = info.Split(':');
                string color;
                if (splitted[0] == "BOUNDARY")
                {
                    string[] coordinates = splitted[1].Split(',');
                    Point p = new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
                    if(Utilities.ImageProcessing.CheckPoint(((Blackboard)agent.Blackboard).GetBoundaries(),p))
                    {
                        string message = Communication.ComposeMarkPixel(int.Parse(coordinates[0]), int.Parse(coordinates[1]), "red");
                        ((Blackboard)agent.Blackboard).AddBoundary(p);
                        ((ScanningAgent)agent).InvokeAction(message);
                    }
                }
                else if (splitted[0] == "EDGE")
                {
                    string[] coordinates = splitted[1].Split(',');
                    Point p = new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
                    if (Utilities.ImageProcessing.CheckPoint(((Blackboard)agent.Blackboard).GetEdges(), p))
                    {
                        string message = Communication.ComposeMarkPixel(int.Parse(coordinates[0]), int.Parse(coordinates[1]), "green");
                        ((Blackboard)agent.Blackboard).AddEdge(p);
                        ((ScanningAgent)agent).InvokeAction(message);
                    }
                }
                else if(splitted[0] == "Mark")
                {
                    ((ScanningAgent)agent).InvokeAction(perception);
                }
            }
        }
    }
}
