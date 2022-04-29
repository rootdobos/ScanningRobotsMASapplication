using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem.Interfaces
{
    public interface IEnvironment
    {
        string GetInformation(string query);
        string ChangeEnvironment(string query);
    }
}
