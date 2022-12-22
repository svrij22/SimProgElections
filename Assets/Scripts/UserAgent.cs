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
    void Start()
    {
        Instance = this;
        UpdateCanvasVOSText();
    }

    /// <summary>
    /// Settings
    /// </summary>

    public void OnSpoilerEffBoolChange(bool val)
    {
        Simulator.VotingStrategy.AdjustForSpoilerEffect = val;
        Simulator.VotingStrategy.RunVotes();
    }

    public void RefreshVoters()
    {
        VoterGenerator.Instance.GenerateVoters();
        Simulator.VotingStrategy.RunVotes();
    }

    public void RefreshParties()
    {
        PartyGenerator.Instance.GenerateParties();
        VoterGenerator.Instance.GenerateVoters();
    }
    public void Vote()
    {
        Simulator.VotingStrategy.RunVotes();
    }

    /// <summary>
    /// Winner Text Changing
    /// </summary>
    
    public GameObject StateText;
    public void UpdateStateText()
    {


        //Update text
        VOSText.GetComponent<TextMeshProUGUI>()
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
                Simulator.VotingStrategy = new InstantRunOff(); 
                break;
            }
            if (Simulator.VotingStrategy.GetType() == typeof(InstantRunOff))
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
