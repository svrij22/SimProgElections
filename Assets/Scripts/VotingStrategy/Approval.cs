
using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.VotingStrategy
{
    public class Approval : IVotingStrategy
    {
        public float RANGE = 7; // HALF of BOARD
        public override void RunVotes()
        {
            //If is finished
            if (IsFinished) return;

            //For each party
            VoteResults.Clear();
            PartyGenerator.Instance.AllParties.ForEach(party =>
            {
                //Instantiate list
                VoteResults.Add(party, new List<VoterScript>());

            });

            //For each Voter
            VoterGenerator.Instance.AllVoters.ForEach(voter =>
            {
                //Get all parties in range
                var allPartiesInRange = GetInRangeForVoter(voter, RANGE);
                allPartiesInRange.ForEach(party =>
                {
                    VoteResults[party].Add(voter);
                });

                //set color
                if (allPartiesInRange.Count > 0)
                    voter.SetColor(allPartiesInRange.First().PartyColour);
            });

            //Set as finished
            IsFinished = true;
        }
    }
}
