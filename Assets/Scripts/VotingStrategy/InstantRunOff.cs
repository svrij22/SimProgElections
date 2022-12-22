
using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.VotingStrategy
{
    public class InstantRunOff : IVotingStrategy
    {
        public bool AdjustForSpoilerEffect { get; set; }
        public void RunVotes()
        {

        }

        public List<PartyScript> GetRankedForVoter(VoterScript voter)
        {
            return new();
        }
        public PartyScript ChooseParty(List<PartyScript> ranked)
        {
            return ranked.First();
        }
    }
}
