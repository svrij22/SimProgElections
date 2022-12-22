using Assets.Scripts.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoterScript : MonoBehaviour
{
    public GameObject Body;

    void Start()
    {

    }
    public Parties VotedParty { get; set; } = Parties.None;
    public void VoteForParty(Parties color)
    {
        //Update colour
        Body.GetComponent<BodyScript>()
            .UpdateColour(color);
    }
}
