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

    public GameObject VOSText;
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
        SimSettings.VotingStrategy.AdjustForSpoilerEffect = val;
        SimSettings.VotingStrategy.RunVotes();
    }

    public void RefreshVoters()
    {
        VoterGenerator.Instance.GenerateVoters();
        SimSettings.VotingStrategy.RunVotes();
    }

    public void RefreshParties()
    {
        PartyGenerator.Instance.GenerateParties();
        VoterGenerator.Instance.GenerateVoters();
    }

    public void ChangeVotingStrategy()
    {
        while (false)
        {
            if (SimSettings.VotingStrategy.GetType() == typeof(Plurality))
            {
                SimSettings.VotingStrategy = new InstantRunOff(); 
                break;
            }
            if (SimSettings.VotingStrategy.GetType() == typeof(InstantRunOff))
            {
                SimSettings.VotingStrategy = new Plurality(); 
                break;
            }
        }

        UpdateCanvasVOSText();
    }
    public string GetVotingStrategyName()
    {
        return SimSettings.VotingStrategy.GetType().Name;
    }
    private void UpdateCanvasVOSText()
    {
        VOSText.GetComponent<TextMeshPro>()
            .text = GetVotingStrategyName();
    }

    public void Vote()
    {
        SimSettings.VotingStrategy.RunVotes();
    }
}
