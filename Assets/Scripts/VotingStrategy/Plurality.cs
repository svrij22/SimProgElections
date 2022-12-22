using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.VotingStrategy
{
    public class Plurality : IVotingStrategy
    {
        public override void RunVotes()
        {
            //Results
            Dictionary<VoterScript, PartyScript> votes = new Dictionary<VoterScript, PartyScript>();

            //For each voter, rank
            VoterGenerator.Instance.AllVoters.ForEach(v =>
            {

                //Get ranked votes
                var rankedVotes = GetRankedForVoter(v);

                //Choose party
                var party = ChooseParty(rankedVotes);

                //Add to dictionary
                votes.Add(v, party);
            });

            //Calculate 2 biggest parties
            Dictionary<PartyScript, int> votesPerParty = new();

            //Count votes
            foreach (var item in votes)
            {
                //Create entry
                if (!votesPerParty.ContainsKey(item.Value)) votesPerParty.Add(item.Value, 0);

                //add value
                votesPerParty[item.Value]++;
            }

            //Order by votes
            var _2biggestParties = votesPerParty
                .OrderBy(p => -p.Value)
                .Select(p => p.Key)
                .Take(2)
                .ToList();

            //Spoiler effect?
            if (AdjustForSpoilerEffect)
            {

                //Remove all votes that arent on the 2 biggest parties
                VoterGenerator.Instance.AllVoters.ForEach(v =>
                {
                    //Get ranked votes
                    var rankedVotes = GetRankedForVoter(v)
                            .Where(r => _2biggestParties.Contains(r))
                            .ToList();

                    //vote
                    v.VoteForParty(rankedVotes.First().PartyColour);
                });
            }
            else
            {
                //Actual vote on best party
                foreach (var item in votes)
                {
                    //Vote for party colour
                    item.Key.VoteForParty(item.Value.PartyColour);
                }
            }

        }

        public override List<PartyScript> GetRankedForVoter(VoterScript voter)
        {
            //Get nearest party
            List<PartyScript> Ranked = new(PartyGenerator.Instance.AllParties);

            //Rank all
            Ranked = Ranked
                .OrderBy(p => Vector3.Distance(voter.transform.position, p.transform.position))
                .ToList();

            return Ranked;
        }
        public override PartyScript ChooseParty(List<PartyScript> ranked)
        {
            return ranked.First();
        }
    }
}