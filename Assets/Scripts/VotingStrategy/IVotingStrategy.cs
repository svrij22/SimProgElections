using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.VotingStrategy
{
    public abstract class IVotingStrategy
    {
        public bool IsFinished { get; set; } = false;
        public bool AdjustForSpoilerEffect { get; set; }

        public Dictionary<PartyScript, List<VoterScript>> VoteResults 
            = new Dictionary<PartyScript, List<VoterScript>>();
        public abstract void RunVotes();

        /// <summary>
        /// Gets ranked votes
        /// </summary>
        /// <returns></returns>
        public Dictionary<PartyScript, int> GetRankedParties()
        {
            return VoteResults
                .ToList()
                .OrderBy(kv => -kv.Value.Count)
                .ToDictionary(kv => kv.Key, kv => kv.Value.Count);
        }

        /// <summary>
        /// Get ranked parties
        /// </summary>
        /// <param name="voter"></param>
        /// <returns></returns>
        public List<PartyScript> GetRankedForVoter(VoterScript voter)
        {
            //Get nearest party
            List<PartyScript> Ranked = new(PartyGenerator.Instance.AllParties);

            //Rank all
            Ranked = Ranked
                .OrderBy(p => Vector3.Distance(voter.transform.position, p.transform.position))
                .ToList();

            return Ranked;
        }

        /// <summary>
        /// get approved parties
        /// </summary>
        /// <param name="voter"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<PartyScript> GetInRangeForVoter(VoterScript voter, float range)
        {
            //Get nearest party
            List<PartyScript> AllParties = new(PartyGenerator.Instance.AllParties);
            List<PartyScript> InRangeParties = new();

            //get if is distance
            AllParties.ForEach(p =>
            {
                var dist = Vector3.Distance(voter.transform.position, p.transform.position);
                if (dist <= range)
                    InRangeParties.Add(p);
            });

            //still return ordered
            return InRangeParties
                .OrderBy(p => Vector3.Distance(voter.transform.position, p.transform.position))
                .ToList(); ;
        }
    }
}
