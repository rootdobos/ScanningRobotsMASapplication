using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Bitmap _MapImage;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bt_addAgent_Click(object sender, EventArgs e)
        {
            ScanningAgent agent = new ScanningAgent(int.Parse( tb_visionRadius.Text), int.Parse(tb_speed.Text));
            _Simulation.AddAgent(tb_agentName.Text, agent);
        }


        private void bt_Load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                if(openFile.ShowDialog()==DialogResult.OK)
                {
                    LoadIamge(openFile.FileName);
                }
            }
        }
        private void LoadIamge(string path)
        {
            _MapImage = new Bitmap(path);
        }
    }
}
