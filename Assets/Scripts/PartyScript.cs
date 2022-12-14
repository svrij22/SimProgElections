using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodyScript;

public class PartyScript : MonoBehaviour
{
    /// <summary>
    /// Bindings
    /// </summary>
    public Colours PartyColour = Colours.Green;

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

}
