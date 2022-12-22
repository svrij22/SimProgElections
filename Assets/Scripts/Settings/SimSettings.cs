using Assets.Scripts.VotingStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Settings
{
    internal class SimSettings
    {
        public static IVotingStrategy VotingStrategy = new Plurality();
    }
}
