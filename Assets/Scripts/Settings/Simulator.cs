using Assets.Scripts.VotingStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Settings
{
    internal class Simulator
    {

        public static bool HasWinner = false;

        public static PartyScript Winner = null;

        public static IVotingStrategy VotingStrategy = new Plurality();

        /// <summary>
        /// Check if everyone has voted
        /// </summary>
        /// <returns></returns>
        public bool EveryoneHasVoted()
        {
            foreach (var v in VoterGenerator.Instance.AllVoters)
                if (v.VotedParty == Parties.None)
                    return false;

            return true;
        }

        /// <summary>
        /// Gets ranked votes
        /// </summary>
        /// <returns></returns>
        public Dictionary<Parties, int> GetRankings()
        {
            Dictionary<Parties, int> nDict = new();
            VoterGenerator.Instance.AllVoters.ForEach(v =>
            {
                if (nDict.ContainsKey(v.VotedParty))
                    nDict.Add(v.VotedParty, 0);
                nDict[v.VotedParty] += 1;
            });
            return nDict;
        }
    }
}
