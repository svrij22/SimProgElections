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
            //Return if voting is already finished
            if (IsFinished) return;

            //Initialize list
            VoteResults = new();
            PartyGenerator.Instance.AllParties.ForEach(p =>
            {
                VoteResults.Add(p, new());
            });

            //For each voter, rank
            VoterGenerator.Instance.AllVoters.ForEach(v =>
            {
                //Get ranked votes
                var rankedVotes = GetRankedForVoter(v);

                //Choose first party
                var party = rankedVotes.First();

                //Vote for party
                v.SetColor(party.PartyColour);

                //Add to dictionary
                VoteResults[party].Add(v);
            });

            //Spoiler effect?
            if (AdjustForSpoilerEffect)
            {

                //Calculate 2 biggest parties
                Dictionary<PartyScript, int> votesPerParty = GetRankedParties();

                //Order by votes
                var _2biggestParties = votesPerParty
                    .OrderBy(p => -p.Value)
                    .Select(p => p.Key)
                    .Take(2)
                    .ToList();

                //Remove all votes that arent on the 2 biggest parties
                VoterGenerator.Instance.AllVoters.ForEach(v =>
                {
                    //Get ranked votes
                    var rankedVotes = GetRankedForVoter(v)
                            .Where(r => _2biggestParties.Contains(r))
                            .ToList();

                    //vote
                    v.SetColor(rankedVotes.First().PartyColour);
                });
            }

            //Set is finished
            IsFinished = true;
        }
    }
}