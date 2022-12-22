using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.VotingStrategy
{
    public abstract class IVotingStrategy
    {
        public bool IsFinished { get; set; } = true;
        public bool AdjustForSpoilerEffect { get; set; }
        public abstract void RunVotes();
        public abstract List<PartyScript> GetRankedForVoter(VoterScript voter);
        public abstract PartyScript ChooseParty(List<PartyScript> ranked);
    }
}
