using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.VotingStrategy
{
    internal interface IVotingStrategy
    {
        public bool AdjustForSpoilerEffect { get; set; }
        public void RunVotes();
        public List<PartyScript> GetRankedForVoter(VoterScript voter);
        public PartyScript ChooseParty(List<PartyScript> ranked);
    }
}
