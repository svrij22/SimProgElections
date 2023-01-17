using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Export
{
    public class CsvController
    {

        /// <summary>
        /// Properties
        /// </summary>
        /// 
        public Csv CsvF = new();

        public int CurrentSimNo;
        private int MaxSimulations { get; set; } = 1000;
        public Simulator Simulator { get; }

        public int StepOfSim = 0;
        public CsvController(Simulator sim)
        {
            Simulator = sim;

            //Write headers
            CsvF.WriteHeaders();
        }

        public bool IsExporting = false;
        public void FixedUpdate()
        {
            if (CurrentSimNo > MaxSimulations)
                return;

            //Unity export in steps
            if (StepOfSim == 0)
            {
                VoterGenerator.Instance.GenerateVoters();
                PartyGenerator.Instance.GenerateParties();
            }
            if (StepOfSim == 1)
            {
                Simulator.VotingStrategy.IsFinished = false;
                Simulator.VotingStrategy.AdjustForSpoilerEffect = false;
                Simulator.VotingStrategy.RunVotes();
            }
            if (StepOfSim == 2)
            {
                CsvF.Write(CurrentSimNo);
            }
            if (StepOfSim == 3)
            {
                Simulator.VotingStrategy.IsFinished = false;
                Simulator.VotingStrategy.AdjustForSpoilerEffect = true;
                Simulator.VotingStrategy.RunVotes();
            }
            if (StepOfSim == 4)
            {
                CsvF.Write(CurrentSimNo);
            }
            if (StepOfSim == 5)
            {
                CurrentSimNo++;
                StepOfSim = -1;
            }
            if (CurrentSimNo > MaxSimulations)
            {
                CsvF.Close();
            }
            StepOfSim++;
        }
    }
}
