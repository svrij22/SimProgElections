using Assets.Scripts.Settings;
using System.Collections;
using System.Collections.Generic;
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

    public void Vote()
    {
        SimSettings.VotingStrategy.RunVotes();
    }
}
