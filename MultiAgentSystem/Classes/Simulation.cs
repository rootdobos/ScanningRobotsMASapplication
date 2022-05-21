using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace MultiAgentSystem.Classes
{
    public class Simulation
    {
        public Interfaces.IEnvironment Environment { get; set; }
        public Interfaces.IBlackboard BlackBoard
        {
            get
            {
                return _Blackboard;
            }
            set
            {
                _Blackboard = value;
            }
        }
        public List<string> AgentsNames
        {
            get
            {
                List<string> output = new List<string>();
                foreach (string name in Agents.Keys)
                {
                    output.Add(name);
                }
                return output;
            }
        }
        public List<string> AgentList
        {
            get
            {
                List<string> output = new List<string>();
                foreach (Interfaces.IAgent agent in Agents.Values)
                {
                    output.Add(agent.ToString());
                }
                return output;
            }
        }
        public Simulation(Interfaces.IBlackboard blackboard)
        {
            _Blackboard = blackboard;
            AgentThreads = new Dictionary<string, Thread>();
            Agents = new Dictionary<string, Interfaces.IAgent>();
        }
        public void AddAgent(string agentName, Interfaces.IAgent agent)
        {
            agent.Blackboard = _Blackboard;
            Thread agentThread = new Thread(new ParameterizedThreadStart(StartAgent));
            Agents.Add(agentName, agent);
            AgentThreads.Add(agentName, agentThread);
            agent.Percept += HandlingPercept;
            agent.Action += HandlingAction;
            if (SimulationStarted)
                agentThread.Start(agent);
        }
        public void StartSimulation()
        {
            if (!SimulationStarted)
            {
                foreach (KeyValuePair<string, Thread> agentThread in AgentThreads)
                {
                    Interfaces.IAgent agent = Agents[agentThread.Key];
                    agentThread.Value.Start(agent);
                }
            }
            else
            {
                foreach (KeyValuePair<string, Thread> agentThread in AgentThreads)
                {
                    Interfaces.IAgent agent = Agents[agentThread.Key];
                    agent.StartAgent();
                }
            }
            SimulationStarted = true;
        }
        public string StopSimulation()
        {
            foreach(string name in Agents.Keys)
            {
                StopAgent(name);
            }
            return _Blackboard.GetStatistics();
        }
        public void StopAgent(string agentName)
        {
            Agents[agentName].StopAgent();
            
        }
        public void RemoveAgent(string agentName)
        {
            StopAgent(agentName);
            Agents[agentName].Percept -= HandlingPercept;
            Agents[agentName].Action -= HandlingAction;
            AgentThreads[agentName].Abort();
            Agents.Remove(agentName);
            AgentThreads.Remove(agentName);

        }
        private Dictionary<string, Thread> AgentThreads;
        private Dictionary<string, Interfaces.IAgent> Agents;
        private bool SimulationStarted=false;
        private Interfaces.IBlackboard _Blackboard;
        protected void StartAgent(object agent)
        {
            ((Interfaces.IAgent)agent).StartAgent();
        }
        protected virtual void HandlingPercept(object sender, PerceptEventArgs args)
        {
            string perception=Environment.GetInformation(args.Query);
            ((Interfaces.IAgent)sender).ProcessPerception(perception);
        }
        protected virtual void HandlingAction(object sender, ActionEventArgs args)
        {
            Environment.ChangeEnvironment(args.ActionDescription);
        }
    }
}
