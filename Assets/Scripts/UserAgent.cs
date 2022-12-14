using Assets.Scripts.Export;
using Assets.Scripts.Settings;
using Assets.Scripts.VotingStrategy;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserAgent : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static UserAgent Instance;

    public Simulator Simulator;

    public Csv CsvF = new();

    void Start()
    {
        //Set singleton
        Instance = this;

        //Create simulator
        Simulator = new();

        //Update canvas text
        UpdateCanvasVOSText();

        CsvF.WriteHeaders();

    }

    public int SimNo;

    private int SimNoMax { get; set; } = 1000;

    public int Step = 0;
    void FixedUpdate()
    {
        if (SimNo > SimNoMax)
            return;

        //Unity export in steps
        if (Step == 0)
        {
            VoterGenerator.Instance.GenerateVoters();
            PartyGenerator.Instance.GenerateParties();
        }
        if (Step == 1)
        {
            Simulator.VotingStrategy.IsFinished = false;
            Simulator.VotingStrategy.AdjustForSpoilerEffect = false;
            Simulator.VotingStrategy.RunVotes();
        }
        if (Step == 2)
        {
            CsvF.Write(SimNo);
        }
        if (Step == 3)
        {
            Simulator.VotingStrategy.IsFinished = false;
            Simulator.VotingStrategy.AdjustForSpoilerEffect = true;
            Simulator.VotingStrategy.RunVotes();
        }
        if (Step == 4)
        {
            CsvF.Write(SimNo);
        }
        if (Step == 5)
        {
            SimNo++;
            Step = -1;
        }
        if (SimNo > SimNoMax)
        {
            CsvF.Close();
        }
        Step++;
    }

    /// <summary>
    /// Settings
    /// </summary>

    public void OnSpoilerEffBoolChange(bool val)
    {
        Simulator.VotingStrategy.IsFinished = false;
        Simulator.VotingStrategy.AdjustForSpoilerEffect = val;
        Simulator.VotingStrategy.RunVotes();
        UpdateStateText();
    }

    public void RefreshVoters()
    {
        Simulator.VotingStrategy.IsFinished = false;
        VoterGenerator.Instance.GenerateVoters();
        Simulator.VotingStrategy.RunVotes();
        UpdateStateText();
    }

    public void RefreshParties()
    {
        Simulator.VotingStrategy.IsFinished = false;
        PartyGenerator.Instance.GenerateParties();
        VoterGenerator.Instance.GenerateVoters();
        UpdateStateText();
    }
    public void Vote()
    {
        Simulator.VotingStrategy.RunVotes();
        UpdateStateText();
    }

    /// <summary>
    /// Winner Text Changing
    /// </summary>
    
    public GameObject StateText;
    public void UpdateStateText()
    {
        var state = string.Empty;
        var atleastOneHasVoted = Simulator.AtleastOneHasVoted();
        var isFinished = Simulator.VotingStrategy.IsFinished;

        //Has everyone voted?
        if (!atleastOneHasVoted && !isFinished)
            state = "Voting is not finished.";
        if (!atleastOneHasVoted && isFinished)
            state = "Not everyone has voted.";
        if (atleastOneHasVoted && isFinished)
            state = Simulator.GetResultsAsString();

        //Update text
        StateText.GetComponent<TextMeshProUGUI>()
            .text = state;
    }


    /// <summary>
    /// Voting Strat changing
    /// </summary>

    public GameObject VOSText;

    public GameObject SpoilerEffectBtn;
    public void ChangeVotingStrategy()
    {
        while (true)
        {
            if (Simulator.VotingStrategy.GetType() == typeof(Plurality))
            {
                SpoilerEffectBtn.SetActive(false);
                Simulator.VotingStrategy = new Approval(); 
                break;
            }
            if (Simulator.VotingStrategy.GetType() == typeof(Approval))
            {
                SpoilerEffectBtn.SetActive(true);
                Simulator.VotingStrategy = new Plurality(); 
                break;
            }
        }

        UpdateCanvasVOSText();
    }
    public string GetVotingStrategyName()
    {
        return Simulator.VotingStrategy.GetType().Name;
    }
    private void UpdateCanvasVOSText()
    {
        VOSText.GetComponent<TextMeshProUGUI>()
            .text = "Voting Strategy: " + GetVotingStrategyName();
    }
}
