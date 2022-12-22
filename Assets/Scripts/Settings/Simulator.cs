using Assets.Scripts.VotingStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Settings
{
    public class Simulator
    {

        /// <summary>
        /// Create voting strategy
        /// </summary>
        public IVotingStrategy VotingStrategy = new Plurality();

        /// <summary>
        /// Check if everyone has voted
        /// </summary>
        /// <returns></returns>
        public bool AtleastOneHasVoted()
        {
            foreach (var v in VoterGenerator.Instance.AllVoters)
                if (v.VotedParty != Parties.None)
                    return true;

            return false;
        }

        /// <summary>
        /// Get rankings as string for UI
        /// </summary>
        /// <returns></returns>
        public string GetResultsAsString()
        {
            var str = "";
            VotingStrategy.GetRankedParties().ToList().ForEach(kv =>
            {
                str += $"{kv.Key.PartyColour} = {kv.Value} - ";
            });
            return str;
        }
    }
}
