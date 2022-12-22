using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Export
{
    public class Csv
    {
        //before your loop
        StringBuilder csv = new StringBuilder();
        public void WriteHeaders()
        {
            csv.AppendLine("Simulation no., Voters, Parties, Voting Strategy, Spoiler Effect, Party Rank, Winning Party, Party Info");
        }

        public void Close()
        {
            //after your loop
            var date = DateTime.Now.Millisecond;
            File.WriteAllText($"C:/UNITY_TEST/SimProgElec_{date}.csv", csv.ToString());
        }

        public void Write(int no)
        {
            var partystring = "";
            foreach (var item in UserAgent.Instance.Simulator.VotingStrategy.GetRankedParties())
            {
                partystring += item.Key.PartyColour + ",";
                partystring += "Position (XYZ): " + ",";
                partystring += item.Key.transform.position.x + ",";
                partystring += item.Key.transform.position.y + ",";
                partystring += item.Key.transform.position.z + ",";
                partystring += "Party is Extreme (%): " + ",";
                var extremePercentage = item.Key.GetPartyPositionPercentage() + ",";
                partystring += extremePercentage;
            }

            csv.AppendLine($"{no}" + "," +
                $"{VoterGenerator.Instance.AllVoters.Count}" + "," +
                $"{PartyGenerator.Instance.AllParties.Count}" + "," +
                $"{UserAgent.Instance.GetVotingStrategyName()}" + "," +
                $"{UserAgent.Instance.Simulator.VotingStrategy.AdjustForSpoilerEffect}" + "," +
                $"{UserAgent.Instance.Simulator.GetResultsAsString()}" + "," +
                $"{UserAgent.Instance.Simulator.VotingStrategy.GetRankedParties().First().Key.PartyColour}" + "," +
                $"{partystring}" + ",");
        }
    }
}
