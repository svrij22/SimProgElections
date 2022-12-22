using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodyScript;

public class PartyScript : MonoBehaviour
{
    /// <summary>
    /// Bindings
    /// </summary>
    public Parties PartyColour = Parties.Green;

    public GameObject Body;

    /// <summary>
    /// Update event
    /// </summary>
    private void Update()
    {
        //Set party colour
        Body.GetComponent<BodyScript>()
            .UpdateColour(PartyColour);
    }

    /// <summary>
    /// Based of its position, gives a percentage on how extreme the party is from 0 to 100
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal double GetPartyPositionPercentage()
    {
        float pos = Math.Abs(transform.position.x) + Math.Abs(transform.position.z);
        return Math.Round((pos / (14f)) * 100f);
    }
}
