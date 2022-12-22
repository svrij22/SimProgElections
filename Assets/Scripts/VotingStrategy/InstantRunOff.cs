
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
        public override void RunVotes()
        {

        }

        public override List<PartyScript> GetRankedForVoter(VoterScript voter)
        {
            return new();
        }
        public override PartyScript ChooseParty(List<PartyScript> ranked)
        {
            return ranked.First();
        }
    }
}
