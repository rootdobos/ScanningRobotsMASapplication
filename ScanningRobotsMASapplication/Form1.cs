using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using MultiAgentSystem.Classes;
using ScanningMAS;
namespace ScanningRobotsMASapplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Blackboard blackboard = new Blackboard();
            _Simulation = new Simulation(blackboard);
        }
        Simulation _Simulation;
        ScanningMAS.Environment _Environment;
        private static System.Timers.Timer aTimer;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bt_addAgent_Click(object sender, EventArgs e)
        {
            ScanningAgent agent = new ScanningAgent(tb_agentName.Text, int.Parse(tb_visionRadius.Text), int.Parse(tb_speed.Text), int.Parse(tb_PositionX.Text), int.Parse(tb_PositionY.Text), (Blackboard)_Simulation.BlackBoard);
            _Simulation.AddAgent(tb_agentName.Text, agent);
            SetAgentTextBox();
        }
        private void SetAgentTextBox()
        {
            List<string> agents = _Simulation.AgentList;
            string txt = "";
            foreach (string agent in agents)
            {
                txt += agent + "\n";
            }
            rtb_Agents.Text = txt;
        }

        private void bt_Load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    LoadIamge(openFile.FileName);
                }
            }
        }
        private void LoadIamge(string path)
        {
            Bitmap map = new Bitmap(path);
            _Environment = new ScanningMAS.Environment(map);
            MainCanvas.Image = _Environment.MarkedPoint;
            pb_map.Image = _Environment.Map;
            MainCanvas.Image.Save(@"C:\alma.png");
            MainCanvas.Visible = true;
            pb_map.Visible = true;
            _Simulation.Environment = _Environment;
            MainCanvas.Invalidate();
            pb_map.Invalidate();
            MainCanvas.Refresh();
            pb_map.Refresh();
            _Environment.Refresh += RefreshScreen;
        }
        private void UpdateWindow(Object source, ElapsedEventArgs e)
        {
            foreach(string operation in _Environment.NextOperation)
            {
                _Environment.ProcessOperation(operation);
            }
            MainCanvas.Invalidate();
        }
        private void RefreshScreen(object sender,ScanningMAS.RefreshEventArgs e)
        {
            lock(e.Locker)
            {
                MainCanvas.Invalidate();
            }
        }
        private void bt_StartSimulation_Click(object sender, EventArgs e)
        {
            _Simulation.StartSimulation();
            aTimer = new System.Timers.Timer(10);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += UpdateWindow;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void bt_StopSimulation_Click(object sender, EventArgs e)
        {
           string log= _Simulation.StopSimulation();
            rtb_Log.Text += log + "\n";
        }
    }
}
