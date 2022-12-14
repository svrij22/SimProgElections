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

    public void VoteForParty(Colours color)
    {
        //Update colour
        Body.GetComponent<BodyScript>()
            .UpdateColour(color);
    }
}
